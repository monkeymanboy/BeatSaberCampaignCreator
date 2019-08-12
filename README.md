# BeatSaberCampaignCreator
An application for creating custom campaigns to be used with the [Custom Campaigns](https://github.com/monkeymanboy/BeatSaberCustomCampaigns) plugin

## Getting Started
Simply click `File>New` and either pick or make an empty folder to create your campaign in to make things simple you can do this in your CustomCampaigns folder

After this mostly everything is explained in the editor

## Cover Image
Place an image called `cover.png` in the folder for your campaign size doesn't matter, just do what looks good and isn't too big

## Background Image
Place an image called `map background.png` in the folder for your campaign size doesn't matter, just do what looks good and fits the map size

The width will be stretched to fit the map area and the height will be adjusted to retain aspect ratio

## Unlockable Items
Create a folder inside your campaign folder called `unlockables` and just place what you want to get unlocked in here

When adding unlockables to maps ensure that you include the file extension

## Unlockable Songs
After checking unlockable song on a given challenge you need to have the map specifically set up for the song to be unlocked

Just add `Complete Campaign Challenge - challenge name` to the requirements field of the map, you can either do this for some or all difficulties just keep in mind the ones without this requirement will always be unlocked

## External Modifiers
This is how you add modifiers from other mods

Currently as of writing this no mods support this feature if you are a modder and want to add support feel free to DM me @monkeymanboy#3669

I will also update this area with how to use this feature with each mod that supports it

## Info
The info screen is an override for the base game's help screen. This doesn't mean you need to use this as a help screen though, use it for whatever you want.
#### Appears Everytime
If this is checked the info screen will appear everytime and if it is unchecked just the first time the challenge is played.
#### Segments
The info screen is broken up into segments each of which can have an image, text, and a seperator.

If you don't want an image or you don't want text for a given segment simply leave it blank. If you don't want a seperator after the given segment then just uncheck that checkbox.

For images you need to create a folder inside your campaign folder called `images` and place them in there then in the `Image File Name` box just write the name of the file, extension included