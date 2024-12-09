using System;
using System.Collections.Generic;

namespace oom2048;

public enum InputAction
{
    None,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    RequestRestart,
    RequestQuit,
    ConfirmRequest,
    CancelRequest,
}

public static class ConsoleInput
{
    private static readonly Dictionary<ConsoleKey, InputAction> InputActionsMapping = new()
    {
        { ConsoleKey.LeftArrow, InputAction.MoveLeft },
        { ConsoleKey.RightArrow, InputAction.MoveRight },
        { ConsoleKey.UpArrow, InputAction.MoveUp },
        { ConsoleKey.DownArrow, InputAction.MoveDown },
        { ConsoleKey.R, InputAction.RequestRestart },
        { ConsoleKey.Q, InputAction.RequestQuit },
        { ConsoleKey.Enter, InputAction.ConfirmRequest },
        { ConsoleKey.Escape, InputAction.CancelRequest },
        { ConsoleKey.Y, InputAction.ConfirmRequest },
        { ConsoleKey.N, InputAction.CancelRequest },
    };

    public static InputAction GetNextInputAction()
    {
        // parameter 'true' means to not print a pressed key in the console
        var key = Console.ReadKey(true).Key;
        return InputActionsMapping.GetValueOrDefault(key, InputAction.None);
    }
}
