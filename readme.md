# Band Tracker App

#### Web app that allows user add clients and stylists, as well as update and delete them, 12/16/2016

#### By Ryan Peterson

## Description

This web app allows the user to add bands and venues to a database.  The user can also update venue info, as well as delete them from the database.

## Setup/Installation Requirements

* Download mono for windows/mac
* Install dnvm via command line/powershell
* Create database: CREATE DATABASE band_tracker; -> back up and restore as band_tracker_test for testing database.
* Create tables:
  * CREATE TABLE bands(id INT IDENTITY(1,1), name VARCHAR(255));
  * CREATE TABLE venues(id INT IDENTITY(1,1), name VARCHAR(255));
  * CREATE TABLE bands_venues(id INT IDENTITY(1,1), band_id INT, venue_id INT);
* Make sure to type in GO after every SQL command when creating db and tables
* Clone git repo at https://github.com/ryanpeterson08/Band-Tracker
* In command line/powershell enter in 'dnx kestrel'
* Go to http://localhost:5004 in your browser


## Specs

| Input                          | Output                                    | Description                                             |
|--------------------------------|-------------------------------------------|---------------------------------------------------------|
| "Crystal Ballroom"             | Venues: {Crystal Ballroom}                | User can add a venue to the venues table                |
| "Crystal Ball Room"            | Venues: {Crystal Ball Room}               | User can edit venue info in venues table                |
| Delete ->  "Crystal Ball Room" | Venues: {}                                | User can delete venue form venues table                 |
| "Black Keys"                   | Bands: {Black Keys}                       | User can add a band to the bands table                  |
| Crystal Ballroom: "Black Keys" | Bands-Venues{Black Keys:Crystal Ballroom} | User can associate a band with a venue                  |
| Black Keys: "Moda Center"      | Bands-Venues{Black Keys: Moda Center}     | User can associate a venue with a band                  |
| Crystal Ballroom -> All Bands  | Bands: {Black Keys}                       | User can see all bands that have played at a that venue |
| Black Keys -> All Venues       | Venues: {Crystal Ballroom, Moda Center}   | User can see all venues a band has played at            |

## Known Bugs

None

## Support and contact details

Email: ryanpeterson08@gmail.com

## Technologies Used

* HTML/CSS
* C#
* SQL
* MS SQL Server
* Nancy web framework

### License

Copyright (c) 2016 Ryan Peterson
