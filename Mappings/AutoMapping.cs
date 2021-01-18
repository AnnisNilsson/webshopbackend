using AutoMapper;

public class AutoMapping : Profile {

    public AutoMapping() {
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();

        CreateMap<OrderRow, OrderDTO>();
        CreateMap<OrderDTO, OrderRow>();

        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();

        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
    }
}