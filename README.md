# Tobii Game Integration with OSC

This project demonstrates the integration of Tobii eye-tracking data with Open Sound Control (OSC) using C#. The application captures gaze points and head pose data from a Tobii eye tracker and sends this data via OSC to a specified IP address and port.

## Overview
Connects to a Tobii eye tracker and retrieves gaze points and head position data. The application continuously sends these as OSC messages to an OSC-compatible application or server.

Sends the following data via OSC:
Gaze X and Y coordinates.
Head position (X, Y, Z).
Whether the user's head is detected.
Whether the user's gaze is focused on the screen.

## Dependencies
Tobii.GameIntegration.Net: To interact with the Tobii eye tracker.
Rug.Osc: To send OSC messages.
