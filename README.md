# Description
This is a program that simulates the standing waves phenomena caused by 2 sinusoidal waves that are pi radians out of phase one to the other, travelling in opposite directions.

A particular case of the interference is created by superpositioning 2 waves that have the same frequency, the same amplitude and are moving in the same support string in opposite directions.
By connecting an elastic string to a fixed point B on an unmovable surface such as a wall, and stretching the string in the normal direction of the surface of the wall to a point A, and oscillating the string up and down at a constant frequency, it is possible to create a standing wave. The standing wave has points in it that have their amplitude of oscillation zero, these being called the nodes, and points with their amplitude being maximum, these being called antinodes.

The initial configuration of the wave can be calculated by the formula:

![Equation 1](https://user-images.githubusercontent.com/30901594/144744653-065016df-0da7-4f05-bc6d-ffc00f7ce0b7.png)

Ap representing the amplitude of each point in the standing wave.
Here, A is the amplitude of the original wave, or the incident wave, lambda representing the wavelength of the incident wave, and x being the distance between the point and the fixed point B.

Now, each point in the string can be simulated by displacing it in the y direction like a normal oscillating wave:

![Equation 2](https://user-images.githubusercontent.com/30901594/144744745-7b55ae05-454b-4c88-8a40-5c02add2016b.png)

Omega is equal to the product of 2pi and the frequency, and frequency is 1/period, and thus the final equation. The period is specified in the program.

In this simulation it is possible to control the period of oscillation, the amplitude of the incident wave, and the wavelength of the incident wave. The simulation can also display or hide the incident wave, the reflected wave with a phase difference of pi, and the resulting standing wave. It also contains the ability to show each individual node and antinode, as well as a guide line to show where the amplitude is zero for easier visualization of the superposition addition of the waves.

The main program is `StandingWaves.cs`, and a sample scene with the program and all the needed UI and settings is in the `Standing Waves.unitypackage` file. It is recommended that you edit the simulation by importing the `.unitypackage` as it already has the sample scene used in the downloadable package.

# Images
![Image 1](https://user-images.githubusercontent.com/30901594/144745170-ad0d7979-2634-4c20-90d0-656318f72769.png)
![Image 2](https://user-images.githubusercontent.com/30901594/144745177-c5d438b5-b53c-43be-915b-3675fa404b35.png)
![Image 3](https://user-images.githubusercontent.com/30901594/144745183-bcce10d2-f957-4425-9a4e-be66cafc5124.png)
