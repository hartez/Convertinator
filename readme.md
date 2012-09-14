# Convertinator #

Convertinator is designed to  allow you to easily define a system of units of measurement and conversions between them. 

## Defining Conversions ##

Conversions between units are defined as a bidirectional graph. This means that you don't have to explicitly define conversions for every possible combination of units - if a path between your two units exists, Convertinator can string together intermediate conversions to make it work.

For example, if we define some units of length:

	var meter = new Unit("meter");
	var kilometer = new Unit("kilometer");
	var foot = new Unit("foot");

And create a conversion graph which specifies conversions from kilometers to meters and meters to feet:

	var system = new ConversionGraph();
	system.AddConversion(
                Conversions.From(kilometer).To(meter).MultiplyBy(1000M),
                Conversions.From(meter).To(foot).MultiplyBy(3.28084M));
	
Convertinator now has enough information to convert from kilometers to feet:

	var measurement = new Measurement(kilometer, 100M);
	Assert.That(system.Convert(measurement, foot) == 328084M); 

And back again:

	Assert.That(system.Convert(new Measurement(foot, 328084M), kilometer) == 100M);


## Units and Names ##

Units in Convertinator can be defined with alternate names and abbreviations for both input and output:
	
	var meter = new Unit("meter")
		.IsAlsoCalled("metre")
		.CanBeAbbreviated("m", "mtr")
		.UsePluralFormat("{0}s");
	
	var feet = new Unit("foot")
		.PluralizeAs("feet")
		.CanBeAbbreviated("ft");

	var meterMeasurement = new Measurement(meter, 1);
	var feetMeasurement = new Measurement(feet, 2);
	
	Assert.That(meterMeasurement.ToAbbreviatedString() == "1 m");
	Assert.That(meterMeasurement.ToString() == "1 meter");
	Assert.That(feetMeasurement.ToAbbreviatedString() == "2 ft");
	Assert.That(feetMeasurement.ToString() == "2 feet");

If pluralizations are specified, they're automatic. For output, you can specify explicit abbreviations or alternate names; for input you can specify as many alternate forms as you want and all of them will work when determining a path for conversion:

	Assert.That(system.Convert(meterMeasurement, feet) == 3.28084M);
	Assert.That(system.Convert(meterMeasurement, "ft") == 3.28084M);
	Assert.That(system.Convert(feetMeasurement, "metre") == 0.6096M);
	Assert.That(system.Convert(feetMeasurement, "mtr") == 0.6096M);
	Assert.That(system.Convert(feetMeasurement, "m") == 0.6096M);

## Presets ##

It's cumbersome to have to define systems from the ground-up every time, so a few preset systems are included (`Length()`, `Volume()`, and `Time()` under `DefaultConfigurations`); those will continue to be fleshed out and expanded in future releases.

## Dependencies ##

The underlying graph and pathfinding in Convertinator are supplied by the excellent [Quickgraph](http://quickgraph.codeplex.com/ "QuickGraph") library.

## Installation ##
You can grab the latest version from [nuget](https://nuget.org/packages/Convertinator/0.0.1.0 "nuget")

## License ##
[MIT License](https://github.com/hartez/Convertinator/blob/master/license.txt "MIT License")
