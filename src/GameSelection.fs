// LICENSE
// This project is proprietary and all rights are reserved by the author.
// Unauthorized copying, distribution, or modification of this project is strictly prohibited.
// Unless You have written permission from the Developer or the FNBUBBLES420 ORG.

module GameSelection

type GameConfig = {
    Name: string
    WindowTitle: string
}

let games = [
    { Name = "Game A"; WindowTitle = "Window A" }
    { Name = "Game B"; WindowTitle = "Window B" }
]

let selectGame index =
    if index < 0 || index >= games.Length then
        failwith "Game index out of range"
    games.[index]
