# Domain Bot

> Pre-release notice and notification!
>
> This is all very much in the exploratory stages and is not at all anywhere close to complete.  Active development is underway and things will change.  You have been warned.

A [domain driven design](https://en.wikipedia.org/wiki/Domain-driven_design) [domain specific language](https://en.wikipedia.org/wiki/Domain-specific_language) based upon [Superpower](https://github.com/datalust/superpower).  **Domain Bot (d-bot)** enables a text-driven approach to DDD modeling with tooling to support model validation, documentation generation, dependency graphing, and more (all of which is coming soon :wink:).

## A quick example

```
system GizmoMaker { 
    description "A sample software system",
    
    aggregate Whatzit {
        description "The best selling thing ever!",
        
        properties {
            string Name,
            int ItemCount,
            Whozit[] Whozits,
            Thingamajig[] Thingamajigs,
            Dohickey Dohickey
        },
        
        behaviors {
            Rename raises WhatzitNameChanged,
            CreateWhozit raises WhozitCreated
        },
        
        service WhatzitService {
            GetByName returns Whatzit,
            GetById => Whatzit
        },
        
        projection WhatzitDto {
            properties {
                string Name,
                int ItemCount
            }
        },
        
        entity Whozit {
            description "It has a face!",
            
            properties {
                string Name,
                string ModeledAfter
            },
            
            events {
                WhozitCreated,
                WhozitRenamed,
                WhozitRemoved
            }
        },
        entity Thingamajig { },
        
        events {
            WhatzitCreated,
            WhatzitNameChanged,
            WhatzitDeleted
        }
    },
    
    value Dohickey { }
}
```

## Ok, seems simple enough ... but why?

I've been working on enhancing a startup-grade production DDD monolith for over a year now, and I've discovered that it can be difficult to effectively describe changes to the application.  I've struggled with articulating improvements and enhancements because, frankly, it's just too easy to prototype by hand in the code base (I know, what a terrible problem to have)!.  However good that problem might seem to be, it isn't scalable for my team - if I want to move beyond being a 1-man architecture crew, I need something easy and repeatable for others.  Enter the DSL and tooling of **d-bot**!

D-Bot provides us with a consistent document format that enables communication of DDD concepts across teams and people.  It is effective in communicating existing software architecture, as well as being easy to hand-tool during the architecture and design phases of the SDLC.

## You've sold me!  What's next?

TBD.  This is an early-stages-idea!  Eventually it'll be published as a `dotnet global tool`, but until then you can fork the code base and go to town.
