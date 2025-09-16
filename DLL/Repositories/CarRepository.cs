using BLL.interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Car car)
        {
            _context.Add(car);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars.ToArrayAsync();
        }

        public void Remove(Car car)
        {
            _context.Remove(car);
        }

        public void Upate(Car car)
        {
            _context.Update(car);
        }
    }
}
