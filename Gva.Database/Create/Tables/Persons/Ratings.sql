print 'Ratings'
GO

CREATE TABLE [dbo].[Ratings] (
    [RatingId]             INT             NOT NULL IDENTITY(1,1),
    [PersonId]             INT             NOT NULL,
    [RatingClassId]        INT             NULL,
    [RatingTypeId]         INT             NULL,
    [AuthorizationId]      INT             NULL,
    [RatingModelId]        INT             NULL,
    [StaffTypeId]          INT             NULL,
    [RatingGroup66Id]      INT             NULL,
    [RatingCategory66Id]   INT             NULL,
    [LocationIndicatorId]  INT             NULL,
    [Sector]               NVARCHAR (200)  NULL,
    [AtsmlRating]          NVARCHAR (200)  NULL,
    [Version]              ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Ratings]                    PRIMARY KEY ([RatingId]),
    CONSTRAINT [FK_Ratings_Persons]            FOREIGN KEY ([PersonId])            REFERENCES [dbo].[Persons]            ([PersonId]),
    CONSTRAINT [FK_Ratings_RatingClasses]      FOREIGN KEY ([RatingClassId])       REFERENCES [dbo].[RatingClasses]      ([RatingClassId]),
    CONSTRAINT [FK_Ratings_RatingTypes]        FOREIGN KEY ([RatingTypeId])        REFERENCES [dbo].[RatingTypes]        ([RatingTypeId]),
    CONSTRAINT [FK_Ratings_Authorizations]     FOREIGN KEY ([AuthorizationId])     REFERENCES [dbo].[Authorizations]     ([AuthorizationId]),
    CONSTRAINT [FK_Ratings_RatingModels]       FOREIGN KEY ([RatingModelId])       REFERENCES [dbo].[RatingModels]       ([RatingModelId]),
    CONSTRAINT [FK_Ratings_StaffTypes]         FOREIGN KEY ([StaffTypeId])         REFERENCES [dbo].[StaffTypes]         ([StaffTypeId]),
    CONSTRAINT [FK_Ratings_RatingGroups66]     FOREIGN KEY ([RatingGroup66Id])     REFERENCES [dbo].[RatingGroups66]     ([RatingGroup66Id]),
    CONSTRAINT [FK_Ratings_RatingCategories66] FOREIGN KEY ([RatingCategory66Id])  REFERENCES [dbo].[RatingCategories66] ([RatingCategory66Id]),
    CONSTRAINT [FK_Ratings_LocationIndicators] FOREIGN KEY ([LocationIndicatorId]) REFERENCES [dbo].[LocationIndicators] ([LocationIndicatorId])
);
GO

exec spDescTable  N'Ratings'                         , N'Класове/Вписани квалификации.'
exec spDescColumn N'Ratings', N'RatingId'            , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Ratings', N'PersonId'            , N'Физическо лице.'
exec spDescColumn N'Ratings', N'RatingClassId'       , N'Клас ВС.'
exec spDescColumn N'Ratings', N'RatingTypeId'        , N'Тип ВС.'
exec spDescColumn N'Ratings', N'AuthorizationId'     , N'Разрешение.'
exec spDescColumn N'Ratings', N'RatingModelId'       , N'Модел на вписаната квалификация.'
exec spDescColumn N'Ratings', N'StaffTypeId'         , N'Тип персонал.'
exec spDescColumn N'Ratings', N'RatingGroup66Id'     , N'Група ВС за AML (Part 66).'
exec spDescColumn N'Ratings', N'RatingCategory66Id'  , N'Категория за AML (Part 66).'
exec spDescColumn N'Ratings', N'LocationIndicatorId' , N'Индикатор за местоположение (Орган по ИКАО).'
exec spDescColumn N'Ratings', N'Sector'              , N'Сектор/работно място.'
exec spDescColumn N'Ratings', N'AtsmlRating'         , N'Степен за ATSML.'
GO
