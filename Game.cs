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
    private readonly Board _board = new();
    private uint _score = 0;

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
        }
    }

    #region Input handlers

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

    private void HandlePlayingState(InputAction inputAction)
    {
        if (inputAction == InputAction.RequestRestart)
        {
            _state = State.ConfirmRestart;
            return;
        }

        if (inputAction == InputAction.RequestQuit)
        {
            _state = State.ConfirmQuit;
            return;
        }

        MoveResult moveResult = inputAction switch
        {
            InputAction.MoveRight => _board.MoveRight(),
            InputAction.MoveLeft => _board.MoveLeft(),
            InputAction.MoveUp => _board.MoveUp(),
            InputAction.MoveDown => _board.MoveDown(),
            _ => MoveResult.None,
        };

        _score += moveResult.Score;

        if (_board.HasMaxValue())
        {
            _state = State.Won;
            return;
        }

        if (!_board.CanMakeMove())
        {
            _state = State.Lost;
            return;
        }

        if (moveResult.HasMoved)
        {
            _board.TryAddRandomTile();
            _board.TryAddRandomTile();
        }
    }

    private void HandleEndGameState(InputAction inputAction)
    {
        if (inputAction == InputAction.ConfirmRequest)
        {
            Reset();
            _state = State.Playing;
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
            Reset();
            _state = State.Playing;
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
        Console.WriteLine($"Score: {_score}");

        if (_state == State.Playing)
        {
            Console.WriteLine("Press arrow keys to move, R to restart, Q to quit.");
        }

        if (_state == State.ConfirmRestart)
        {
            Console.WriteLine("Are you sure you want to restart (Y/N)?");
        }

        if (_state == State.ConfirmQuit)
        {
            Console.WriteLine("Are you sure you want to quit (Y/N)?");
        }

        if (_state == State.Won || _state == State.Lost)
        {
            if (_state == State.Won)
            {
                Console.Write("You won! ");
            }

            if (_state == State.Lost)
            {
                Console.Write("You lost! ");
            }

            Console.WriteLine("Press Y to restart or N to quit.");
        }

        Console.WriteLine(_board.ToString());
    }

    private void Reset()
    {
        _board.Reset();
        _score = 0;
    }
}
