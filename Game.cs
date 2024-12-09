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
        Quit,
    }

    private State _state = State.Playing;

    public Game()
    {
    }

    public void Run()
    {
        while (_state != State.Quit)
        {
            Console.Clear();
            PrintState();

            var inputAction = ConsoleInput.GetNextInputAction();
            HandleInput(inputAction);
            Console.WriteLine($"Input action: {inputAction}");
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
                HandlePlayingState(inputAction);
                break;
            case State.Won:
            case State.Lost:
                HandleEndGameState(inputAction);
                break;
            case State.ConfirmRestart:
                HandleConfirmRestart(inputAction);
                break;
            case State.ConfirmQuit:
                HandleConfirmQuit(inputAction);
                break;
            default:
                // we should never reach here, but just in case we must know something is wrong
                throw new InvalidOperationException($"Unhandled state: {_state}");
        }
    }

    #region Input handlers

    private void HandlePlayingState(InputAction inputAction)
    {
        switch (inputAction)
        {
            case InputAction.MoveUp:
            case InputAction.MoveDown:
            case InputAction.MoveLeft:
            case InputAction.MoveRight:
                throw new NotImplementedException();
                break;
            case InputAction.RequestRestart:
                _state = State.ConfirmRestart;
                break;
            case InputAction.RequestQuit:
                _state = State.ConfirmQuit;
                break;
        }
    }

    private void HandleEndGameState(InputAction inputAction)
    {
        if (inputAction == InputAction.ConfirmRequest)
        {
            _state = State.Playing;
            // todo: reset game state
        }

        if (inputAction == InputAction.CancelRequest)
        {
            _state = State.Quit;
        }
    }

    private void HandleConfirmRestart(InputAction inputAction)
    {
        if (inputAction == InputAction.ConfirmRequest)
        {
            _state = State.Playing;
            // todo: reset game state
        }

        if (inputAction == InputAction.CancelRequest)
        {
            _state = State.Playing;
        }
    }

    private void HandleConfirmQuit(InputAction inputAction)
    {
        if (inputAction == InputAction.ConfirmRequest)
        {
            _state = State.Quit;
        }

        if (inputAction == InputAction.CancelRequest)
        {
            _state = State.Playing;
        }
    }

    #endregion

    private void PrintState()
    {
        Console.WriteLine($"State: {_state}");
    }
}
