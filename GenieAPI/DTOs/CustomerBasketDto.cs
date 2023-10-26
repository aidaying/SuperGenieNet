namespace GenieAPI.DTOs;

public class CustomerBasketDto
{
    public String Id { get; set; }
    public List<BasketItemDto> items { get; set; } = new List<BasketItemDto>();
}
