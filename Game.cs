using System;

namespace oom2048;

public class Game
{
    private enum State
    {
        Playing,
        Won,
        Lost,
        ConfirmRestart,
        ConfirmQuit,
    }

    private State _state = State.Playing;
    private bool _shouldQuit = false;

    public Game()
    {
    }

    public void Run()
    {
        while (!_shouldQuit)
        {
            var inputAction = ConsoleInput.GetNextInputAction();
            HandleInput(inputAction);

            Console.Clear();
            Console.WriteLine($"Input action: {inputAction}");
            PrintState();
        }
    }

    private void HandleInput(InputAction inputAction)
    {
        if (inputAction == InputAction.None)
        {
            return;
        }

        switch (_state)
        {
            case State.Playing:
                if (inputAction == InputAction.RequestQuit)
                {
                    _state = State.ConfirmQuit;
                }
                else
                {
                    throw new NotImplementedException();
                }
                break;
            case State.Won:
            case State.Lost:
            case State.ConfirmRestart:
                break;
            case State.ConfirmQuit:
                if (inputAction == InputAction.ConfirmRequest)
                {
                    _shouldQuit = true;
                }

                if (inputAction == InputAction.CancelRequest)
                {
                    _state = State.Playing;
                }

                break;
            default:
                // we should never reach here, but just in case we must know something is wrong
                throw new NotImplementedException($"Unhandled state: {_state}");
        }
    }

    private void PrintState()
    {
        Console.WriteLine($"State: {_state}");
    }
}
