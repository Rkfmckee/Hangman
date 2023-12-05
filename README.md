# Hangman

## Create a game

### Request

`POST /Game`

### Response

HTTP 200 OK
{
	"gameId": "7c2f7ff2-43fe-409c-1fae-08dbf5ec2a59"
}

## Get game

### Request

`GET /Game/{id}`

### Response

HTTP 200 OK
{
	"gameStatus": "In progress",
	"word": "_ _ _ _ _ _ _",
	"incorrectGuessesRemaining": 6,
	"guesses": ""
}

## Delete game

### Request

`DELETE /Game/{id}`

### Response

HTTP 204 NO CONTENT

## Get all games

### Request

`GET /Games`

### Response

HTTP 200 OK
{
	"3bf04d77-926c-41b6-ced6-08dbf5c92f8e": "Lost",
	"def470b7-d43f-47fa-1faf-08dbf5ec2a59": "In progress"
}

## Guess

### Request

`POST /Game/{id}/Guess/{guessChar}`

### Response

HTTP 200 OK
{
	"guessCorrect": true,
	"word": "_ _ D _ _ _ _",
	"incorrectGuessesRemaining": 5,
	"guesses": "A, D"
}