﻿namespace PrismAPI.Graphics;

/// <summary>
/// Gradient class, used for generating gradients.
/// Reference: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
/// </summary>
public unsafe static class Gradient
{
	/// <summary>
	/// Creates a new gradient drawn on an instance of a <see cref="Canvas"/> object.
	/// </summary>
	/// <param name="Width">Width (in pixels) of the gradient.</param>
	/// <param name="Height">Height (in pixels) of the gradient.</param>
	/// <param name="Start">The starting color.</param>
	/// <param name="End">The end color.</param>
	/// <returns>A new instance of a graphics object.</returns>
	public static Canvas GetGradient(ushort Width, ushort Height, Color Start, Color End)
	{
		Canvas Temp = new(Width, Height);

		for (int I = 0; I < Height; I++)
		{
			Temp.DrawFilledRectangle(0, I, Width, 1, 0, Color.Lerp(Start, End, 1.0f / Height * I));
		}

		return Temp;
	}

	/// <summary>
	/// Creates a new gradient drawn on an instance of a <see cref="Canvas"/> object.
	/// </summary>
	/// <param name="Width">Width (in pixels) of the gradient.</param>
	/// <param name="Height">Height (in pixels) of the gradient.</param>
	/// <param name="Colors">The colors to generate in the canvas.</param>
	/// <returns>A new instance of a graphics object.</returns>
	public static Canvas GetGradient(ushort Width, ushort Height, Color[] Colors)
	{
		Canvas Temp = new(Width, Height);

		// Calculate the height 'delta', it is the total width per gradient pair.
		int HeightDelta = Height / Colors.Length;

		// Loop over each color to draw in the gradient.
		for (int I1 = 0; I1 < Colors.Length - 1; I1++)
		{
			// Go over each line that the gradient will fill.
			for (int I2 = 0; I2 < HeightDelta; I2++)
			{
				// Get the interpolated color. It's calculated based on 'I1' and delta height index 'I2'.
				Color Calculated = Color.Lerp(Colors[I1], Colors[I1 + 1], 1.0f / HeightDelta * I2);

				// Fill this line with the correct color across with 1 pixel height.
				Temp.DrawFilledRectangle(0, HeightDelta * I1 + I2, Width, 1, 0, Calculated);
			}
		}

		return Temp;
	}

	/// <summary>
	/// Creates a new gradient drawn on an instance of a <see cref="Canvas"/> object.
	/// </summary>
	/// <param name="Width">Width (in pixels) of the gradient.</param>
	/// <param name="Height">Height (in pixels) of the gradient.</param>
	/// <param name="ElapsedMS">The total time passed in the gradient.</param>
	/// <returns>A new instance of a graphics object.</returns>
	public static Canvas GetGradient(ushort Width, ushort Height, uint ElapsedMS)
	{
		Canvas Temp = new(Width, Height);

		// Loop over all pixels.
		for (int X = 0; X < Width; X++)
		{
			for (int Y = 0; Y < Height; Y++)
			{
				// Normalized pixel coordinates (from 0 to 1)
				int UVY = Y / Height;
				int UVX = X / Width;

				// Time varying pixel color
				float R = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UVX);
				float G = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UVY + 2);
				float B = 0.5f + 0.5f * MathF.Cos(ElapsedMS + UVY + 4);

				// Output to screen
				Temp[X, Y] = new(255, R * 255, G * 255, B * 255);
			}
		}

		return Temp;
	}
}