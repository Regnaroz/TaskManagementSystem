﻿using SampleProject.Domain.Entities;

namespace SampleProject.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Lookup, LookupDto>();
        }
    }
}
