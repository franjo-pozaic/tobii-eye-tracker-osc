// See https://aka.ms/new-console-template for more information
using Tobii.GameIntegration.Net;
using System;
using System.Threading;
using System.Drawing;
using Rug.Osc;
using System.Net;

Console.WriteLine("Hello, World!");
TobiiGameIntegrationApi.SetApplicationName("Plato");



var info = TobiiGameIntegrationApi.GetTrackerInfos();
var isTrackerEnabled = TobiiGameIntegrationApi.IsTrackerEnabled();
Console.WriteLine($"Tracker found: {info[0].FriendlyName} | Enabled in Tobii software: {isTrackerEnabled}");

var isInitialized = TobiiGameIntegrationApi.IsApiInitialized();
Console.WriteLine(isInitialized);

TobiiGameIntegrationApi.Update();
isInitialized = TobiiGameIntegrationApi.IsApiInitialized();
Console.WriteLine(isInitialized);

var rectangle = new TobiiRectangle
{
    Left = 0,
    Top = 0,
    Right = 1920,
    Bottom = 1080
};
TobiiGameIntegrationApi.TrackRectangle(rectangle);
IPAddress address = IPAddress.Parse("127.0.0.1");
int port = 10000;

const int SLEEP_TIME = 1000 / 20;

using (OscSender sender = new OscSender(address, port))
{
    sender.Connect();
    while (true)
    {
        Thread.Sleep(SLEEP_TIME);
        TobiiGameIntegrationApi.Update();
        GazePoint gazePoint;
        var data = TobiiGameIntegrationApi.TryGetLatestGazePoint(out gazePoint);
        var isPresent = TobiiGameIntegrationApi.IsPresent();
        sender.Send(new OscMessage("/x", gazePoint.X));
        sender.Send(new OscMessage("/y", gazePoint.Y));
        sender.Send(new OscMessage("/head_lost", !isPresent));
        sender.Send(new OscMessage("/focus_on_screen", isPresent));

        HeadPose headPose;
        TobiiGameIntegrationApi.TryGetLatestHeadPose(out headPose);

        sender.Send(new OscMessage("/head_x", headPose.Position.X));
        sender.Send(new OscMessage("/head_y", headPose.Position.Y));
        sender.Send(new OscMessage("/head_z", headPose.Position.Z));

        Console.WriteLine($"Enabled: {isTrackerEnabled}, present: {isPresent}, coordinates: {gazePoint.X} {gazePoint.Y}");
    }
}