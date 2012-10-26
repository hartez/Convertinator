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

## Systems ##

Convertinator lets you tag a unit with a system name (e.g., "US", "metric", "imperial", etc.). Once units are tagged with system names, you can convert a value to a destination system (instead of a specific unit). This lets you handle the situation where you don't really care what unit you get, as long as it's in the preferred system.

For example:

	var graph = new ConversionGraph();
	
	var meter = new Unit("meter").SystemIs("metric");
	var mile = new Unit("mile").SystemIs("US");
	var feet = new Unit("foot").SystemIs("US");
	var kilometer = new Unit("kilometer").SystemIs("metric").HasCounterPart(mile);
	
	graph.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));
	graph.AddConversion(Conversions.One(kilometer).In(meter).Is(1000M));
	graph.AddConversion(Conversions.One(mile).In(feet).Is(5280M));

With this graph configured, you can now do things like this:

	var result = graph.ConvertSystem(new Measurement("meter", 1M), "US");

    result.Value.Should().Be(3.2808M);
    result.Unit.Name.Should().Be("foot");

Convertinator started with "meter" and found the shortest path to another unit tagged with the system "US". 

But what about "natural" conversions? When most people think of converting meters to US units, they assume a conversion to feet. But when converting kilometers to US units, they usually assume a conversion to miles. 

That's where explicit counterparts come in. Notice that when we specified 'kilometer' above, we also specified 'mile' as its counterpart. If Convertinator can find a path to an explicit counterpart, it will use that conversion. So this works:

	var result = graph.ConvertSystem(new Measurement("kilometer", 1M), "US");
	
	result.Value.Should().Be(0.6214M);
	result.Unit.Name.Should().Be("mile");

## Other Stuff ##

Check out the unit tests for examples of other stuff, like [currency conversion](https://github.com/hartez/Convertinator/blob/master/Convertinator.Tests/Currencies.cs) and multi-step conversions (like [temperatures](https://github.com/hartez/Convertinator/blob/master/Convertinator.Tests/TemperatureTests.cs)).

## Presets ##

It's cumbersome to have to define systems from the ground-up every time, so a few preset systems are included (`Length()`, `Volume()`, and `Time()` under `DefaultConfigurations`); those will continue to be fleshed out and expanded in future releases.

## Debugging ##

With sufficiently complex graphs, sometimes it's helpful to be able to visualize the graph in order to debug your configuration. The ConversionGraph class has a ToDotFile() method which outputs .dot files for use with tools like [GraphViz](http://www.graphviz.org/). Take a look at the SampleImageGeneration project for an example of using GraphViz on the output generated by this method.

## Web Projects ##
If you're using this in an ASP.NET project, you may encounter a strong name validation error loading up some of the QuickGraph Contracts assemblies. This is a result of some weirdness with how Contracts assemblies are being handled in the QuickGraph NuGet package. If this happens, you can fix it by modifying the <compilation> section of <system.web> in your web.config like this:

	<compilation debug="true" targetFramework="4.0">
      <assemblies>
        <remove assembly="QuickGraph.Contracts"></remove>
        <remove assembly="QuickGraph.Graphviz.Contracts"></remove>
        <remove assembly="QuickGraph.Serialization.Contracts"></remove>
        <remove assembly="QuickGraph.Data.Contracts"></remove>
      </assemblies>
    </compilation>

## Dependencies ##

The underlying graph and pathfinding in Convertinator are supplied by the excellent [Quickgraph](http://quickgraph.codeplex.com/ "QuickGraph") library.

## Installation ##
You can grab the latest version from [nuget](https://nuget.org/packages/Convertinator "nuget")

## License ##
[MIT License](https://raw.github.com/hartez/Convertinator/master/license.txt "MIT License")
