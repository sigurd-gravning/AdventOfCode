module day1

open System.IO

// Shared helpers
let private parseRotation (position: int) (rotation: string) =
    let direction = rotation.[0]
    let amount = int rotation.[1..]
    match direction with
    | 'L' -> position - amount
    | 'R' -> position + amount
    | _ -> failwith "Unknown direction"

let private checkCorrectPos pos =
    pos % 100 = 0

let private countCrossings oldPos newPos =
    abs((newPos / 100) - (oldPos / 100))

// Read file
let private readInput() =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "..", "Data", "input-day1.txt")
    File.ReadAllLines(path) |> Array.toList

let secretEntrance =
    let rotations = readInput()

    let secretPassword (startPosition: int) rotations =
        let rec loop rotations position count =
            match rotations with
            | [] -> count
            | head :: tail ->
                let rotation = parseRotation position head
                if checkCorrectPos rotation then loop tail rotation (count + 1)
                else loop tail rotation count
        loop rotations startPosition 0

    secretPassword 100050 rotations

let newPasswordMethod =
    let rotations = readInput()

    let secretPassword (startPosition: int) rotations =
        let rec loop rotations position count =
            match rotations with
            | [] -> count
            | head :: tail ->
                let newPosition = parseRotation position head
                let crossings = countCrossings position newPosition
                if checkCorrectPos position && head[0] = 'L' && checkCorrectPos newPosition then
                    loop tail newPosition (count + crossings)
                else if checkCorrectPos position && head[0] = 'L' then
                    loop tail newPosition (count + crossings - 1)
                else if head[0] = 'L' && checkCorrectPos newPosition then
                    loop tail newPosition (count + crossings + 1)
                else
                    loop tail newPosition (count + crossings)
        loop rotations startPosition 0

    secretPassword 100050 rotations