using BLL.interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Task.Services
{
    public class CarService
    {
        private readonly ICarRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CarService(IUnitOfWork unitOfWork, ICarRepository carRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = carRepository;
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task AddAsync(Car car)
        {
            try
            {
                _repository.Add(car);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception){

            }
        }
        public async Task Remove(Car car)
        {
            _repository.Remove(car);
            _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Car car)
        {
            _repository.Upate(car);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
