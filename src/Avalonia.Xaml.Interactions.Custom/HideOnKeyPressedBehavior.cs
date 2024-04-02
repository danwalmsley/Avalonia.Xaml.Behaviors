﻿using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Custom;

/// <summary>
/// A behavior that allows to hide control on key down event.
/// </summary>
public class HideOnKeyPressedBehavior : Behavior<Control>
{
    /// <summary>
    /// Identifies the <seealso cref="TargetControl"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<Control?> TargetControlProperty =
        AvaloniaProperty.Register<HideOnKeyPressedBehavior, Control?>(nameof(TargetControl));

    /// <summary>
    /// Identifies the <seealso cref="Key"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<Key> KeyProperty =
        AvaloniaProperty.Register<HideOnKeyPressedBehavior, Key>(nameof(Key), Key.Escape);

    /// <summary>
    /// Gets or sets the target control. This is a avalonia property.
    /// </summary>
    [ResolveByName]
    public Control? TargetControl
    {
        get => GetValue(TargetControlProperty);
        set => SetValue(TargetControlProperty, value);
    }

    /// <summary>
    /// Gets or sets the key. This is a avalonia property.
    /// </summary>
    public Key Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <inheritdoc />
    protected override void OnAttachedToVisualTree()
    {
        AssociatedObject?.AddHandler(InputElement.KeyDownEvent, AssociatedObject_KeyDown,
            RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    /// <inheritdoc />
    protected override void OnDetachedFromVisualTree()
    {
        AssociatedObject?.RemoveHandler(InputElement.KeyDownEvent, AssociatedObject_KeyDown);
    }

    private void AssociatedObject_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key && TargetControl is not null)
        {
            TargetControl.IsVisible = false;
        }
    }
}