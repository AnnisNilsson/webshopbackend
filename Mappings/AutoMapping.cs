using AutoMapper;

public class AutoMapping : Profile {

    public AutoMapping() {
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();

        CreateMap<OrderRow, OrderRowDTO>();
        CreateMap<OrderRowDTO, OrderRow>();

        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();

        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
    }
}