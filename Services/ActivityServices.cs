using LifeOs.DTOs;
using LifeOs.Entities;
using LifeOs.Interfaces;
using AutoMapper;

namespace LifeOs.Services
{
    public class ActivityServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IActivityRepository _activityRepository;

        public ActivityServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            IActivityRepository activityRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _activityRepository = activityRepository;
        }

        public async Task<int> CreateActivityAsync(ActivityCreateDto dto, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            int calculatedXP = (int)(dto.DurationMinutes * category.XPMultiplier * 10);

            var activity = _mapper.Map<UserActivity>(dto);
            activity.UserId = userId;
            activity.EarnedXP = calculatedXP;

            await _activityRepository.AddAsync(activity);
            await _unitOfWork.CommitAsync();

            return calculatedXP;
        }
    }
}