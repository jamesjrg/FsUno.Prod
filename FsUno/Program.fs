﻿open FsUno.Domain

open CommandHandlers
open Deck
open Game
open EventStore

[<EntryPoint>]
let main _ = 
    
    let eventHandler = new EventHandler()
    use store = 
        EventStore.create()
        |> subscribe eventHandler.Handle


    let handle = Game.create (readStream store) (appendToStream store)

    let gameId = GameId 1

    handle (StartGame { GameId = gameId; PlayerCount = 4; FirstCard = Digit(digit 3, Red)})
    
    handle (PlayCard { GameId = gameId; Player = 0; Card = Digit(digit 3, Blue) })

    handle (PlayCard { GameId = gameId; Player = 1; Card = Digit(digit 8, Blue) })
    
    handle (PlayCard { GameId = gameId; Player = 2; Card = Digit(digit 8, Yellow) })
    
    handle (PlayCard { GameId = gameId; Player = 3; Card = Digit(digit 4, Yellow) })
    
    handle (PlayCard { GameId = gameId; Player = 0; Card = Digit(digit 4, Green) })


    System.Console.ReadLine() |> ignore

    0 // return an integer exit code
