using System.Threading;

namespace DeepBlue.Models;

/// <summary>
/// Represents a level in a dive profile, including depth, time, gas used, and whether it is a decompression stop.
/// </summary>
public class DiveLevel
{
    /// <summary>
    /// Gets or sets the depth of the dive level in meters.
    /// </summary>
    public double depth { get; set; }

    /// <summary>
    /// Gets or sets the time spent at this dive level in minutes.
    /// </summary>
    public int time { get; set; }

    /// <summary>
    /// Gets or sets the gas used at this dive level.
    /// </summary>
    public Gas gas { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this dive level is a decompression stop.
    /// </summary>
    public bool isDecoStop { get; set; }
    
    /// <summary>
    /// Depth in ATM.
    /// </summary>
    public double ATM { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DiveLevel"/> class with the specified depth, time, gas, and decompression stop indicator.
    /// </summary>
    /// <param name="depth">The depth of the dive level in meters.</param>
    /// <param name="time">The time spent at this dive level in minutes.</param>
    /// <param name="gas">The gas used at this dive level.</param>
    /// <param name="isDecoStop">A value indicating whether this dive level is a decompression stop.</param>
    public DiveLevel(double depth, int time, Gas gas, bool isDecoStop)
    {
        this.depth = depth;
        this.time = time;
        this.gas = gas;
        this.isDecoStop = isDecoStop;
        this.ATM = (depth / 10.0) + 1.0;
    }
}