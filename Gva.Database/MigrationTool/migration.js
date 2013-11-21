/*jshint node: true */
/*jshint maxlen: 130 */
'use strict';
var oracle = require('oracle');
var fs = require('fs');
var _ = require('lodash');

var connectData = { 'hostname': '192.168.0.19', 'user': 'system', 'password': 'DBSYSTEMVENI', 'database': 'VENI.CAA' };


var idMap = {};
var currentNomValueId = 1;
var nomId = 1;
var templatesPath = __dirname + '/templates/';
var resultsPath = __dirname + '/../Insert/Noms/';
/*jshint -W101*/
/*jshint -W109*/
var specs = [
	{
		nomName : "Полове",
		nomAlias: 'gender',
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_SEX"
	},
	{
		nomName : "Държави",
		nomAlias: "countries",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, CA_NATIONALITY_CODE, HEADING, HEADING_TRANS, CA_LICENCE_CODE from CAA_DOC.NM_COUNTRY"
	},
	{
		nomName : "Населени места",
		nomAlias: "cities",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, COUNTRY_ID, TV_TYPE, NOTES, POSTAL_CODE_DEFAULT, OBL_CODE, OBST_CODE from CAA_DOC.NM_TOWN_VILLAGE"
	},
	{
		nomName : "Типове адреси",
		nomAlias: "addressTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, ADDRESS_TYPE from CAA_DOC.NM_ADDRESS_TYPE"
	},
	{
		nomName : "Типове персонал",
		nomAlias: "staffTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, CA_CODE from CAA_DOC.NM_STAFF_TYPE"
	},
	{
		nomName : "Категории персонал",
		nomAlias: "employmentCategories",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, STAFF_TYPE_ID, CA_CODE, DATE_FROM, DATE_TO from CAA_DOC.NM_JOB_CATEGORY"
	},
	{
		nomName : "Степени на образование",
		nomAlias: "graduations",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, RATING from CAA_DOC.NM_GRADUATION"
	},
	{
		nomName : "Учебни заведения",
		nomAlias: "schools",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(2000)) NAME, CAST(NAME_TRANS AS NVARCHAR2(2000)) NAME_TRANS, VALID_YN, PILOT_TRAINING, GRADUATION_ID, GRADUATION_ID_LIST from CAA_DOC.SCHOOL"
	},
	{
		nomName : "Направления",
		nomAlias: "directions",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME from CAA_DOC.NM_DIRECTION"
	},
	{
		nomName : "Типове документи",
		nomAlias: "documentTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, ID_DIRECTION, PERSON_ONLY from CAA_DOC.NM_DOCUMENT_TYPE"
	},
	{
		nomName : "Роли документи",
		nomAlias: "documentRoles",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, ID_DIRECTION, PERSON_ONLY, CAST(CATEGORY_CODE AS NVARCHAR2(10))CATEGORY_CODE from CAA_DOC.NM_DOCUMENT_ROLE"
	},
	{
		nomName : "Типове документи за самоличност на Физическо лице",
		nomAlias: "personIdDocumentTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_DOCUMENT_TYPE where code in ('3','4','5')"
	},
	{
		nomName : "Роли документи за проверка на Физическо лице",
		nomAlias: "personCheckDocumentRoles",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, ID_DIRECTION, PERSON_ONLY, CAST(CATEGORY_CODE AS NVARCHAR2(10))CATEGORY_CODE from CAA_DOC.NM_DOCUMENT_ROLE where CATEGORY_CODE = 'T'"
	},
	{
		nomName : "Типове документи за проверка на Физическо лице",
		nomAlias: "personCheckDocumentTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_DOCUMENT_TYPE where code not in ('3','4','5')"
	},
	{
		nomName : "Типове състояния на Физичеко лице",
		nomAlias: "personStatusTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(MEANING AS NVARCHAR2(255)) NAME, CAST(MEANING_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_SHORT_LISTS where domain = 'STATE_REASON'"
	},
	{
		nomName : "Издатели на документи - Други",
		nomAlias: "otherDocPublishers",
		query : "select ID,  CAST(NAME AS NVARCHAR2(255)) NAME, VALID_YN from CAA_DOC.NM_OTHER_PUBLISHERS"
	},
	{
		nomName : "Издатели на документи - Медицински",
		nomAlias: "medDocPublishers",
		query : "select ID,  CAST(NAME AS NVARCHAR2(255)) NAME, VALID_YN from CAA_DOC.NM_MED_PUBLISHERS"
	},
	{
		nomName : "Типове ВС за екипажи",
		nomAlias: "ratingTypes",
		query : "select ID, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, CA_CODE, DATE_FROM, DATE_TO from CAA_DOC.NM_RATING_TYPE"
	},
	{
		nomName : "Групи Класове ВС за екипажи",
		nomAlias: "ratingClassGroups",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, STAFF_TYPE_ID from CAA_DOC.NM_RATING_CLASS_GROUP"
	},
	{
		nomName : "Класове ВС за екипажи",
		nomAlias: "ratingClasses",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, GROUP_ID, CA_CODE, DATE_FROM, DATE_TO from CAA_DOC.NM_RATING_CLASS"
	},
	{
		nomName : "Подкласове ВС за екипажи",
		nomAlias: "ratingSubClasses",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(DESCRIPTION AS NVARCHAR2(255)) NAME, VALID_YN, CLASS_ID from CAA_DOC.NM_SUBCLASS_ATSM"
	},
	{
		nomName : "Групи Разрешения към квалификация",
		nomAlias: "authorizationGroups",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, STAFF_TYPE_ID, CLASS_GROUP_ID from CAA_DOC.NM_AUTHORIZATION_GROUP"
	},
	{
		nomName : "Разрешения към квалификация",
		nomAlias: "authorizations",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, GROUP_ID, CA_CODE, DATE_FROM, DATE_TO from CAA_DOC.NM_AUTHORIZATION"
	},
	{
		nomName : "Легенда към видове(типове) правоспособност",
		nomAlias: "licenceTypeDictionary",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, STAFF_TYPE_ID from CAA_DOC.LICENCE_DICTIONARY"
	},
	{
		nomName : "Видове(типове) правоспособност",
		nomAlias: "licenceTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, STAFF_TYPE_ID, DATE_FROM, DATE_TO, CA_CODE, SEQ_NO, PRT_MAX_RATING_COUNT, PRT_MAX_MED_CERT_COUNT, LICENCE_DICTIONARY_ID, LICENCE_DICTIONARY1_ID, LICENCE_DICTIONARY2_ID, LICENCE_CODE, QLF_CODE  from CAA_DOC.NM_LICENCE_TYPE"
	},
	{
		nomName : "Индикатори на местоположение",
		nomAlias: "locationIndicators",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(POSITION AS NVARCHAR2(255)) NAME, CAST(POSITION_TRANS AS NVARCHAR2(255)) NAME_TRANS, FLAG_YN from CAA_DOC.NM_INDICATOR"
	},
	{
		nomName : "Държатели на ТС за ВС",
		nomAlias: "aircraftTCHolders",
		query : "select ID, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_TC_HOLDER"
	},
	{
		nomName : "Типове ВС",
		nomAlias: "aircraftTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, CAST(DESCRIPTION AS NVARCHAR2(400)) DESCRIPTION from CAA_DOC.NM_AC_TYPE"
	},
	{
		nomName : "Групи ВС",
		nomAlias: "aircraftTypeGroups",
		query : "select ID, CAST(CODE AS NVARCHAR2(255)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, VALID_YN, ID_AC_TYPE, ID_TC_HOLD from CAA_DOC.NM_AC_GROUP"
	},
	{
		nomName : "Категория за АМЛ (Part-66 Category)",
		nomAlias: "aircraftGroup66",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_AC_66"
	},
	{
		nomName : "Клас (Part-66 Category)",
		nomAlias: "aircraftClases66",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(DESCRIPTION AS NVARCHAR2(255)) NAME, VALID_YN, ID_AC_66, CATEGORY from CAA_DOC.NM_CATEGORY_66"
	},
	{
		nomName : "Ограничения (Part-66)",
		nomAlias: "limitations66",
		query : "select ID, CAST(CODE AS NVARCHAR2(255)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, GENERAL from CAA_DOC.NM_LIMITATIONS_66"
	},
	{
		nomName : "Класове за медицинска годност",
		nomAlias: "medClasses",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_MED_CLASS"
	},
	{
		nomName : "Ограничения за медицинска годност",
		nomAlias: "medLimitation",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_MED_LIMITATION"
	},
	{
		nomName : "Роли в натрупан летателният опит",
		nomAlias: "experienceRoles",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, CA_CODE from CAA_DOC.NM_EXPERIENCE_ROLE"
	},
	{
		nomName : "Видове летателен опит",
		nomAlias: "experienceMeasures",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, CA_CODE from CAA_DOC.NM_EXPERIENCE_MEASURE"
	},
	{
		nomName : "Въздухоплавателни администрации",
		nomAlias: "caa",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN, COUNTRY_ID, HEADING, HEADING_TRANS, SUBHEADING, SUBHEADING_TRANS from CAA_DOC.CAA"
	},
	{
		nomName : "Степени на квалификационен клас на Физическо лице",
		nomAlias: "personRatingLevels",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(MEANING AS NVARCHAR2(255)) NAME, CAST(MEANING_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_SHORT_LISTS where domain = 'ATSML_STEPEN'"
	},
	{
		nomName : "Видове действия относно правоспособност",
		nomAlias: "licenceActions",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_LICENCE_ACTION"
	},
	{
		nomName : "Класификатор на организации",
		nomAlias: "organizationTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, VALID_YN from CAA_DOC.NM_FIRM_TYPE"
	},
	{
		nomName : "Видове организации",
		nomAlias: "organizationKinds",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(MEANING AS NVARCHAR2(255)) NAME, CAST(MEANING_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_SHORT_LISTS where domain = 'TYPE_ORG'"
	},
	{
		nomName : "Видове заявления",
		nomAlias: "applicationTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, DATE_FROM, DATE_TO, TIME_LIMIT, DIRECTION_ID, LICENCE_TYPES from CAA_DOC.NM_REQUEST_TYPE"
	},
	{
		nomName : "Видове плащания по заявления",
		nomAlias: "applicationpaymentTypes",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS, DATE_FROM, DATE_TO from CAA_DOC.NM_PAYMENT_TYPE"
	},
	{
		nomName : "Парични единици",
		nomAlias: "currencies",
		query : "select ID, CAST(CODE AS NVARCHAR2(30)) CODE, CAST(NAME AS NVARCHAR2(255)) NAME, CAST(NAME_TRANS AS NVARCHAR2(255)) NAME_TRANS from CAA_DOC.NM_CURRENCY"
	}
];
/*jshint +W101*/
/*jshint +W109*/

function Context() {
	var self = this;
	
	self.$str = function (value) {
		// if(value && /(?:[\r\n])/.test(value.toString())){
			// console.log(value);
		// }
		return value ? 'N\'' + value.toString().replace(/(?:\r\n|\n|\r)/g , ' ').replace(/(?:')/g , '\'\'') + '\'' : 'NULL';
	};
	
	self.$int = function (value) {
		return value ? value : 'NULL';
	};
	
	self.$bool = function (value) {
		return value ? (value === 'Y' ? 1 : 0) : 'NULL';
	};
	
	self.$json = function (value) {
		for (var val in value) {
			if (typeof value[val] === 'string') {
				value[val] = value[val].toString().replace(/(?:\r\n|\n|\r)/g , '\\n').replace(/(?:')/g , '\'\'');
			}
		}
		return 'N\'' + JSON.stringify(value) + '\'';
	};
}

var start;

oracle.connect(connectData, function(err, connection) {
	function processSpec() {
		var spec = specs.shift();
		
		if (spec) {
			var context = new Context();
			context.idMap = idMap;
			context.nomName = spec.nomName;
			context.nomAlias = spec.nomAlias;
			idMap[context.nomAlias] = {};
		
			connection.execute(spec.query , [], function(err, results) {
			
				if ( err ) {
					console.log(err);
				}
				else {
					context.nomId = nomId;
					for (var j = 0; j < results.length; j++) {
						idMap[context.nomAlias][results[j].ID] = currentNomValueId;
						results[j].nomValueId = currentNomValueId;
						results[j].nomId = context.nomId;
						currentNomValueId++;
					}
					nomId++;
					context.results = results;
					
					if(fs.existsSync(resultsPath + spec.nomAlias + '.sql')) {
						fs.truncateSync(resultsPath + spec.nomAlias + '.sql', 0);
					}
					var nomValuesTemplate = fs.readFileSync(templatesPath + spec.nomAlias + '.txt', 'utf8');
					var nomValuesResult = _.template(nomValuesTemplate, context);
					context.content = nomValuesResult;
					var _wrapperTemplate = fs.readFileSync(templatesPath + '_wrapper.txt', 'utf8');
					var _wrapperResult = _.template(_wrapperTemplate, context);
					fs.appendFileSync(resultsPath + spec.nomAlias + '.sql','\ufeff' + _wrapperResult);
					
					processSpec();
				}
			});
		}
		else {
			var d = new Date();
			var end = d.getTime();
			console.log('Total time : ' + ((end - start) / 1000) + ' sec');
			connection.close();
		}
	}
	
	if ( err ) {
		console.log(err);
	} else {
		var d = new Date();
		start = d.getTime();
		if(!fs.existsSync(resultsPath)) {
			fs.mkdirSync(resultsPath);
		}
		processSpec();
	}
});
