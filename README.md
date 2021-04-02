# HAL
The C#/.NET Core implementation of Hypertext Application Language (HAL) specification. This project provides the full support of JSON Hypertext Application Language which is described [here](https://tools.ietf.org/html/draft-kelly-json-hal-08).

[![Build status](https://dev.azure.com/sunnycoding/HAL/_apis/build/status/HAL-CI)](https://dev.azure.com/sunnycoding/HAL/_build/latest?definitionId=11)

## What is HAL?
According to the [official site of HAL](http://stateless.co/hal_specification.html), HAL is a simple format that gives a consistent and easy way to hyperlink between resources in a RESTful API.

## HAL Specification
The HAL specification documentation can be found [here](https://tools.ietf.org/html/draft-kelly-json-hal-08).

## Implementation Model
Following class diagram shows the HAL model that has been implemented by this project.
![HAL class diagram](https://raw.githubusercontent.com/daxnet/hal/master/docs/HalClassDiagram.png)

## How to Use
### Adding the Package

- Refer to [https://www.nuget.org/packages/Hal](https://www.nuget.org/packages/Hal) for the instructions of installing the NuGet package.


### Example
The following C# Fluent API code:
```cs
var builder = new ResourceBuilder();
var resource = builder
    .WithState(new { currentlyProcessing = 14, shippedToday = 20 })
    .AddSelfLink().WithLinkItem("/orders")
    .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
    .AddLink("next").WithLinkItem("/orders?page=2")
    .AddLink("ea:find").WithLinkItem("/orders{?id}", templated: true)
    .AddEmbedded("ea:order")
        .Resource(new ResourceBuilder()
            .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
            .AddSelfLink().WithLinkItem("/orders/123")
            .AddLink("ea:basket").WithLinkItem("/baskets/98712")
            .AddLink("ea:customer").WithLinkItem("/customers/7809"))
        .Resource(new ResourceBuilder()
            .WithState(new { total = 20.00F, currency = "USD", status = "processing" })
            .AddSelfLink().WithLinkItem("/orders/124")
            .AddLink("ea:basket").WithLinkItem("/baskets/97213")
            .AddLink("ea:customer").WithLinkItem("/customers/12369"))
    .Build();

var json = resource.ToString();

```

Will result in the following HAL JSON:
```json
{
    "_links": {
        "self": { "href": "/orders" },
        "curies": [{ "name": "ea", "href": "http://example.com/docs/rels/{rel}", "templated": true }],
        "next": { "href": "/orders?page=2" },
        "ea:find": {
            "href": "/orders{?id}",
            "templated": true
        }
    },
    "currentlyProcessing": 14,
    "shippedToday": 20,
    "_embedded": {
        "ea:order": [{
            "_links": {
                "self": { "href": "/orders/123" },
                "ea:basket": { "href": "/baskets/98712" },
                "ea:customer": { "href": "/customers/7809" }
            },
            "total": 30.00,
            "currency": "USD",
            "status": "shipped"
        }, {
            "_links": {
                "self": { "href": "/orders/124" },
                "ea:basket": { "href": "/baskets/97213" },
                "ea:customer": { "href": "/customers/12369" }
            },
            "total": 20.00,
            "currency": "USD",
            "status": "processing"
        }]
    }
}

```

Or you can use the object model below instead of the Fluent API to generate the same result:
```cs
var links = new LinkCollection
{
    new Link("self") { Items = new LinkItemCollection { new LinkItem("/orders") } },
    new Link("curies") { Items = new LinkItemCollection(true)
        {
            new LinkItem("http://example.com/docs/rels/{rel}") { Name = "ea", Templated = true }
        }
    },
    new Link("next") { Items = new LinkItemCollection { new LinkItem("/orders?page=2") } },
    new Link("ea:find")
    {
        Items = new LinkItemCollection { new LinkItem("/orders{?id}") { Templated = true } }
    }
};

var embedded = new EmbeddedResource
{
    Name = "ea:order",
    Resources = new ResourceCollection
    {
        new Resource(new { total = 30.00F, currency = "USD", status = "shipped" })
        {
            Links = new LinkCollection
            {
                new Link("self")
                {
                    Items = new LinkItemCollection { new LinkItem("/orders/123") }
                },
                new Link("ea:basket")
                {
                    Items = new LinkItemCollection { new LinkItem("/baskets/98712") }
                },
                new Link("ea:customer")
                {
                    Items = new LinkItemCollection { new LinkItem("/customers/7809") }
                },
            }
        },
        new Resource(new { total = 20.00F, currency = "USD", status = "processing" })
        {
            Links = new LinkCollection
            {
                new Link("self")
                {
                    Items = new LinkItemCollection { new LinkItem("/orders/124") }
                },
                new Link("ea:basket")
                {
                    Items = new LinkItemCollection { new LinkItem("/baskets/97213") }
                },
                new Link("ea:customer")
                {
                    Items = new LinkItemCollection { new LinkItem("/customers/12369") }
                },
            }
        }
    }
};

var resource = new Resource(new { currentlyProcessing = 14, shippedToday = 20 })
{
    Links = links,
    EmbeddedResources = new EmbeddedResourceCollection { embedded }
};

var hal = resource.ToString();
```
