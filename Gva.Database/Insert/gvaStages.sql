INSERT [dbo].[GvaStages] 
    ([GvaStageId],[Name], [Alias])
VALUES
    (1, N'Новопостъпило'         , N'new'          ),
    (2, N'В обработка'           , N'processing'   ),
    (3, N'Нови документи'        , N'newDocuments' ),
    (4, N'Отказано'              , N'declined'     ),
    (5, N'Одобрено'              , N'approved'     ),
    (6, N'Готов лиценз'          , N'licenceReady' ),
    (7, N'Предаден на заявителя' , N'returned'     ),
    (8, N'Приключено'            , N'done'         )
GO