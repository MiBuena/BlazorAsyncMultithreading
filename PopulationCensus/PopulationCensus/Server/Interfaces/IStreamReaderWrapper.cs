﻿namespace PopulationCensus.Server.Interfaces
{
    public interface IStreamReaderWrapper
    {
        StreamReader GetStreamReader(IFormFile file);

        StreamReader GetStreamReader(string path);
    }
}
