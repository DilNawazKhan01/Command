using System;
using System.Collections.Generic;

// Command Interface
public interface ICommand
{
    void Execute();
}

// Concrete Command for turning the light on
public class LightOnCommand : ICommand
{
    private readonly Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

// Concrete Command for turning the light off
public class LightOffCommand : ICommand
{
    private readonly Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}

// Concrete Command for increasing the thermostat temperature
public class ThermostatIncreaseCommand : ICommand
{
    private readonly Thermostat _thermostat;

    public ThermostatIncreaseCommand(Thermostat thermostat)
    {
        _thermostat = thermostat;
    }

    public void Execute()
    {
        _thermostat.IncreaseTemperature();
    }
}

// Concrete Command for decreasing the thermostat temperature
public class ThermostatDecreaseCommand : ICommand
{
    private readonly Thermostat _thermostat;

    public ThermostatDecreaseCommand(Thermostat thermostat)
    {
        _thermostat = thermostat;
    }

    public void Execute()
    {
        _thermostat.DecreaseTemperature();
    }
}

// Receiver class - Light
public class Light
{
    private bool _isOn = false;

    public void TurnOn()
    {
        _isOn = true;
        Console.WriteLine("Light is ON");
    }

    public void TurnOff()
    {
        _isOn = false;
        Console.WriteLine("Light is OFF");
    }
}

// Receiver class - Thermostat
public class Thermostat
{
    private int _temperature = 20; // Initial temperature

    public void IncreaseTemperature()
    {
        _temperature++;
        Console.WriteLine($"Thermostat temperature increased to {_temperature}");
    }

    public void DecreaseTemperature()
    {
        _temperature--;
        Console.WriteLine($"Thermostat temperature decreased to {_temperature}");
    }
}

// Invoker class - RemoteController
public class RemoteController
{
    private readonly List<ICommand> _commands = new List<ICommand>();

    public void SetCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void PressButton(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < _commands.Count)
        {
            _commands[buttonIndex].Execute();
        }
        else
        {
            Console.WriteLine("Invalid button press. Please try again.");
        }
    }
}

// Client class - Program
class Program
{
    static void Main()
    {
        // Create IoT devices
        Light livingRoomLight = new Light();
        Thermostat livingRoomThermostat = new Thermostat();

        // Create commands
        ICommand lightOnCommand = new LightOnCommand(livingRoomLight);
        ICommand lightOffCommand = new LightOffCommand(livingRoomLight);
        ICommand thermostatIncreaseCommand = new ThermostatIncreaseCommand(livingRoomThermostat);
        ICommand thermostatDecreaseCommand = new ThermostatDecreaseCommand(livingRoomThermostat);

        // Create RemoteController
        RemoteController remoteController = new RemoteController();

        // Set commands for buttons (key presses)
        remoteController.SetCommand(lightOnCommand);            // Button 0
        remoteController.SetCommand(lightOffCommand);           // Button 1
        remoteController.SetCommand(thermostatIncreaseCommand); // Button 2
        remoteController.SetCommand(thermostatDecreaseCommand); // Button 3

        // User interaction
        Console.WriteLine("Press a button to control IoT devices:");
        Console.WriteLine("0 - Turn Light On");
        Console.WriteLine("1 - Turn Light Off");
        Console.WriteLine("2 - Increase Thermostat Temperature");
        Console.WriteLine("3 - Decrease Thermostat Temperature");
        Console.WriteLine("Press any other key to exit.");

        while (true)
        {
            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int buttonIndex))
            {
                remoteController.PressButton(buttonIndex);
            }
            else
            {
                break; // Exit the program if an invalid key is pressed
            }
        }
    }
}
