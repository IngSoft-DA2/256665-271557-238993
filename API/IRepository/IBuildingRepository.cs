﻿using Domain;

namespace IRepository;

public interface IBuildingRepository
{
    public IEnumerable<Building> GetAllBuildings();
}