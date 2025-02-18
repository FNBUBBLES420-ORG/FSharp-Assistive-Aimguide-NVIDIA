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
