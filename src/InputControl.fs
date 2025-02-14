module InputControl

open System
open System.Threading
open WindowsInput
open WindowsInput.Native

let simulator = new InputSimulator()
let rand = Random()

// Easing function for smooth movement
let easeOutQuad (t: float) = t * t

// Randomized human-like movement
let moveMouseSmooth (startX: int) (startY: int) (endX: int) (endY: int) (duration: int) =
    let steps = 20 + rand.Next(10)  // Random number of steps between 20 and 30
    let dx = float (endX - startX) / float steps
    let dy = float (endY - startY) / float steps

    for i in 1..steps do
        let t = float i / float steps
        let easedT = easeOutQuad t  // Apply easing
        let newX = int (startX + dx * easedT * float steps)
        let newY = int (startY + dy * easedT * float steps)

        simulator.Mouse.MoveMouseBy(newX - startX, newY - startY)
        Thread.Sleep(rand.Next(5, 15))  // Small delay to mimic reaction time

// Simulate random delay before aiming
let humanDelay () =
    let delay = rand.Next(30, 150)  // Random delay in milliseconds
    Thread.Sleep(delay)

// Example usage
let aimAtTarget (targetX: int) (targetY: int) =
    let (startX, startY) = (0, 0)  // Replace with actual mouse position
    humanDelay()  // Add a delay before movement
    moveMouseSmooth startX startY targetX targetY 500  // Move over 500ms
