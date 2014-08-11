﻿// Copyright (c) Pawel Kadluczka, Inc. All rights reserved. See License.txt in the project root for license information.

namespace CodeFirstStoreFunctions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Core.Mapping;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics;
    using System.Linq;

    public class FunctionsConvention : IStoreModelConvention<EntityContainer>
    {
        private readonly string _defaultSchema;
        private readonly Type _methodClassType;

        public FunctionsConvention(string defaultSchema, Type methodClassType)
        {
            _defaultSchema = defaultSchema;
            _methodClassType = methodClassType;
        }

        public void Apply(EntityContainer item, DbModel model)
        {
            var functionDescriptors = new FunctionDiscovery(model, _methodClassType).FindFunctions();
            var storeFunctionBuilder = new StoreFunctionBuilder(model, _defaultSchema);

            foreach (var functionDescriptor in functionDescriptors)
            {
                var storeFunctionDefinition = storeFunctionBuilder.Create(functionDescriptor);
                model.StoreModel.AddItem(storeFunctionDefinition);

                if (functionDescriptor.StoreFunctionKind != StoreFunctionKind.ScalarUserDefinedFunction)
                {
                    var functionImportDefinition = CreateFunctionImport(model, functionDescriptor);
                    model.ConceptualModel.Container.AddFunctionImport(functionImportDefinition);

                    List<FunctionImportResultMapping> resultMappings = new List<FunctionImportResultMapping>();
                    if (functionDescriptor.ReturnTypes.All(t => t is EntityType || t is ComplexType))
                    {
                        foreach (EdmType returnType in functionDescriptor.ReturnTypes)
                        {
                            FunctionImportStructuralTypeMapping typeMapping;
                            if (returnType is EntityType)
                            {
                                var entityType = (EntityType)returnType;

                                var returnTypePropertyMappings = new Collection<FunctionImportReturnTypePropertyMapping>();
                                foreach (var propertyMapping in model.GetEntityTypePropertyMappings(entityType).OfType<ScalarPropertyMapping>())
                                {
                                    returnTypePropertyMappings.Add(new FunctionImportReturnTypeScalarPropertyMapping(propertyMapping.Property.Name, propertyMapping.Column.Name));
                                }

                                typeMapping = new FunctionImportEntityTypeMapping(
                                    Enumerable.Empty<EntityType>(),
                                    new[] { entityType },
                                    returnTypePropertyMappings,
                                    Enumerable.Empty<FunctionImportEntityTypeMappingCondition>());
                            }
                            else // ComplexType
                            {
                                var complexType = (ComplexType)returnType;

                                var returnTypePropertyMappings = new Collection<FunctionImportReturnTypePropertyMapping>();
                                foreach (var property in complexType.Properties)
                                {
                                    returnTypePropertyMappings.Add(new FunctionImportReturnTypeScalarPropertyMapping(property.Name, property.Name));
                                }

                                typeMapping = new FunctionImportComplexTypeMapping(complexType, returnTypePropertyMappings);
                            }

                            FunctionImportResultMapping resultMapping = new FunctionImportResultMapping();
                            resultMappings.Add(resultMapping);
                            resultMapping.AddTypeMapping(typeMapping);
                        }
                    }

                    if (functionImportDefinition.IsComposableAttribute)
                    {
                        model.ConceptualToStoreMapping.AddFunctionImportMapping(
                            new FunctionImportMappingComposable(
                                functionImportDefinition,
                                storeFunctionDefinition,
                                resultMappings.FirstOrDefault() ?? new FunctionImportResultMapping(),
                                model.ConceptualToStoreMapping));
                    }
                    else
                    {
                        model.ConceptualToStoreMapping.AddFunctionImportMapping(
                            new FunctionImportMappingNonComposable(
                                functionImportDefinition,
                                storeFunctionDefinition,
                                resultMappings.Any() ? resultMappings.ToArray() : new FunctionImportResultMapping[0],
                                model.ConceptualToStoreMapping));
                    }
                }
            }

            // TODO: model defined functions?
        }

        private static EdmFunction CreateFunctionImport(DbModel model, FunctionDescriptor functionImport)
        {
            EntitySet[] entitySets;
            FunctionParameter[] returnParameters;
            CreateReturnParameters(model, functionImport, out returnParameters, out entitySets);

            var functionPayload =
                new EdmFunctionPayload
                {
                    Parameters =
                        functionImport
                            .Parameters
                            .Select(
                                p => FunctionParameter.Create(p.Name, p.EdmType,
                                        p.IsOutParam ? ParameterMode.InOut : ParameterMode.In))
                            .ToList(),
                    ReturnParameters = returnParameters,
                    IsComposable = functionImport.StoreFunctionKind == StoreFunctionKind.TableValuedFunction,
                    IsFunctionImport = true,
                    EntitySets = entitySets
                };

            return EdmFunction.Create(
                functionImport.Name,
                model.ConceptualModel.Container.Name,
                DataSpace.CSpace,
                functionPayload,
                null);
        }

        private static void CreateReturnParameters(DbModel model, FunctionDescriptor functionImport,
            out FunctionParameter[] returnParameters, out EntitySet[] entitySets)
        {
            var resultCount = functionImport.ReturnTypes.Count();
            entitySets = new EntitySet[resultCount];
            returnParameters = new FunctionParameter[resultCount];

            for (int i = 0; i < resultCount; i++)
            {
                var returnType = functionImport.ReturnTypes[i];

                if (returnType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                {
                    var types = Tools.GetTypeHierarchy(returnType);

                    var matchingEntitySets =
                        model.ConceptualModel.Container.EntitySets
                            .Where(s => types.Contains(s.ElementType))
                            .ToArray();

                    if (matchingEntitySets.Length == 0)
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                "The model does not contain EntitySet for the '{0}' entity type.",
                                returnType.FullName));
                    }

                    Debug.Assert(matchingEntitySets.Length == 1, "Invalid model (MEST)");

                    entitySets[i] = matchingEntitySets[0];
                }

                returnParameters[i] = FunctionParameter.Create(
                    "ReturnParam" + i,
                    returnType.GetCollectionType(),
                    ParameterMode.ReturnValue);
            }
        }
    }
}