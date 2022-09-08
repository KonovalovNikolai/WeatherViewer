namespace OpenMeteoApi {
    public enum WeatherCodes {
        // Clear
        ClearSky = 0,
        // Clouds
        MainlyClear = 1, 
        PartlyCloudy = 2, 
        Overcast = 3,
        // Fog
        Fog = 45, 
        RimFog = 48,
        // Drizzle
        LightDrizzle = 51,
        ModerateDrizzle = 53,
        DenseDrizzle = 55,
        // Freezing drizzle
        LightFreezingDrizzle = 56,
        DenseFreezingDrizzle = 57,
        // Rain
        SlightRain = 61, 
        ModerateRain = 63, 
        HeavyRain = 65,
        // Freezing rain
        LightFreezingRain = 66, 
        HeavyFreezingRain = 67,
        // Snow fall
        SlightSnowFall = 71, 
        ModerateSnowFall = 72,
        // Snow grains
        SnowGrains = 77,
        // Rain showers
        SlightRainShower = 80, 
        ModerateRainShower = 81,
        ViolentRainShower = 82,
        // 	Snow showers
        SlightSnowShower = 85, 
        HeavySnowShower = 86,
        // Thunderstorm
        Thunderstorm = 95,
        // Thunderstorm with Hail
        SlightThunderstormHail = 96, 
        HeavyThunderstormHail = 99,
    }
}
