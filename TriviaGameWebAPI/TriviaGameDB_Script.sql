USE [TriviaGameDB]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 03-Feb-20 14:46:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[ChoiceId] [uniqueidentifier] NULL,
	[AnswerDuration] [int] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Choice]    Script Date: 03-Feb-20 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Choice](
	[ChoiceId] [uniqueidentifier] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[ChoiceName] [varchar](50) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
 CONSTRAINT [PK_Choice] PRIMARY KEY CLUSTERED 
(
	[ChoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 03-Feb-20 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[GameId] [uniqueidentifier] NOT NULL,
	[GenreId] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 03-Feb-20 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreId] [uniqueidentifier] NOT NULL,
	[GenreName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 03-Feb-20 14:46:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [uniqueidentifier] NOT NULL,
	[GenreId] [uniqueidentifier] NOT NULL,
	[QuestionNumber] [int] NOT NULL,
	[QuestionDescription] [varchar](500) NOT NULL,
	[QuestionDuration] [int] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Choice] FOREIGN KEY([ChoiceId])
REFERENCES [dbo].[Choice] ([ChoiceId])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Choice]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Game] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([GameId])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Game]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Question]
GO
ALTER TABLE [dbo].[Choice]  WITH CHECK ADD  CONSTRAINT [FK_Choice_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[Choice] CHECK CONSTRAINT [FK_Choice_Question]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([GenreId])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_Genre]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([GenreId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Genre]
GO

---------------------------------------------------------------------------------------------------------------

declare @genreId uniqueidentifier=NEWID()
declare @questionId uniqueidentifier=NEWID()

--Genre 1

INSERT INTO [dbo].[Genre]
           ([GenreId]
           ,[GenreName])
     VALUES
           (@genreId,
           'Action')

--Question 1

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			1,
			'In what film does Robert Patrick potray a robot?',
            20)

--Question 1 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Terminator 1',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Terminator 7',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Terminator 2',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Terminator 3',
           0)


--Question 2

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			2,
			'The movie Gladiator, was directed by whom?',
            20)

--Question 2 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Jonh Black',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'David Scott',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Ridley Scott',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Peter Scott',
           0)

--Question 3

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			3,
			'What was Imhotep condemned to in the film The Mummy?',
            20)

--Question 3 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Dia',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Hom',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Hom Dai',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Hom Dar',
           0)

--Question 4

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			4,
			'Who was Ashe Corven in The Crow: City of Angels?',
            20)

--Question 4 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Vincent Black',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Peter Black',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Vincent Perez',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Ryne Perez',
           0)

--Question 5

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			5,
			'In the movie Top Gun starring Tom Cruise, in what ocean is the aircraft carrier located?',
            20)

--Question 5 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'USA',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Africa',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Indian',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Japan',
           0)

----------------------------------------------------------------------------------

--Genre 2

set @genreId=NEWID()

INSERT INTO [dbo].[Genre]
           ([GenreId]
           ,[GenreName])
     VALUES
           (@genreId,
           'Comedy')

--Question 1

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			1,
			'Where does Hannah meet her fiance Colin in the movie Made of Honor?',
            20)

--Question 1 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'India',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Japan',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Scotland',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'China',
           0)


--Question 2

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			2,
			'What is Rob Reiner character name in This is Spinal Tap?',
            20)

--Question 2 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Jonh Black',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'David Scott',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Marty Dibergi',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Peter Scott',
           0)

--Question 3

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			3,
			'Which 2005 movie used this promo line: He is back?',
            20)

--Question 3 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Herbie',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Fully Loaded',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Herbie: Fully Loaded',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Hom Dar',
           0)

--Question 4

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			4,
			'What car was featured in the 1977 movie Smokey and the Bandit?',
            20)

--Question 4 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '1971 Pontiac Firebird',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '1972 Pontiac Firebird',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '1977 Pontiac Firebird',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '1975 Pontiac Firebird',
           0)

--Question 5

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			5,
			'In what movie did Aretha Franklin sing Respect and Think?',
            20)

--Question 5 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Blues',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Brothers',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Blues Brothers',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'The Reds',
           0)



----------------------------------------------------------------------------------

--Genre 3

set @genreId=NEWID()

INSERT INTO [dbo].[Genre]
           ([GenreId]
           ,[GenreName])
     VALUES
           (@genreId,
           'Horror')

--Question 1

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			1,
			'Who was the villain in the original Friday the 13th?',
            20)

--Question 1 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Elias Voorhees',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Voorhees',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Pamela Voorhees',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Dian Kimble',
           0)


--Question 2

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			2,
			'A Nightmare on Elm Street takes place where?',
            20)

--Question 2 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Springwood, California',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'New Jersey',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Springwood, Ohio',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'NYC',
           0)

--Question 3

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			3,
			'Who did Captain Elliot Spencer become?',
            20)

--Question 3 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Herbie',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Tall Man',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Pinhead',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Pighead',
           0)

--Question 4

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			4,
			'How many Michael Myers Halloween movies are there?',
            20)

--Question 4 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '8',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '4',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '9',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           '10',
           0)

--Question 5

set @questionId = NEWID()

INSERT INTO [dbo].[Question]
           ([QuestionId]
           ,[GenreId]
		   ,[QuestionNumber]
           ,[QuestionDescription]
           ,[QuestionDuration])
     VALUES
           (@questionId,
            @genreId,
			5,
			'What is the name of the camp from Sleepaway Camp?',
            20)

--Question 5 Choices

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Camp Blues',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Camp Flabanabba',
           0)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Camp Arawak',
           1)

INSERT INTO [dbo].[Choice]
           ([ChoiceId]
           ,[QuestionId]
           ,[ChoiceName]
           ,[IsCorrect])
     VALUES
           (NEWID(),
           @questionId,
           'Jerryboree',
           0)