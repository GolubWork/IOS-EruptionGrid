# EntityViews
Модуль EntityViews позволяет:
Привязывать визуальные представления к ECS-сущностям
Поддерживает как загрузку из путей, так и работу с prefab'ами
Отделяет ответственность за визуализацию от логики

## BindViewFeature
BindViewFeature — это точка входа, объединяющая все системы, которые отвечают за связывание представлений с сущностями.

Эти системы реализуют паттерн View Binding:
- По пути
- По префабу

### Таблица используемых систем
| **Система**                          | **Контекст** | **Сущность Содержит** | **Сущность Не содержит** | **Изменяемые компоненты**     | **Назначение**                              |
|-------------------------------------|--------------|-----------------------|--------------------------|-------------------------------|---------------------------------------------|
| BindEntityViewFromPrefabSystem      | Game         | `ViewPrefab`          | `View`                   | `View`                        | Создаёт EntityView из prefab                |
| BindEntityViewFromPathSystem        | Game         | `ViewPath`            | `View`, `ViewProcessed`  | `View`, `ViewProcessed`       | Создаёт EntityView из адреса (Addressables) |
| BindAudioEntityViewFromPrefabSystem | Audio        | `ViewPrefab`          | `View`                   | `View`                        | Создаёт AudioView из prefab                 |
| BindAudioEntityViewFromPathSystem   | Audio        | `ViewPath`            | `View`, `ViewProcessed`  | `View`, `ViewProcessed`       | Создаёт AudioView из адреса (Addressables)  |


## SelfInitializedEntityView
Компонент, который сам создает и инициализирует GameEntity при Awake().

Полезно для создания сущностей "на месте" без участия фабрик или внешних систем.

## ViewComponents
Компоненты, используемые ECS-системами для связывания сущностей с представлениями.

- Общие
    - ViewProcessed — маркер, что View уже привязан
  - ViewPath — путь к представлению (например, в Resources)
- Game (для GameEntity)
  - View — ссылка на поведение IEntityBehaviour<GameEntity>
  - ViewPrefab — prefab поведения GameEntityBehaviour
- Audio (для AudioEntity)
    - View — аналогично, но для AudioEntityBehaviour
  - ViewPrefab — prefab поведения AudioEntityBehaviour


## Adapter
Папка Adapters содержит реализацию адаптеров представлений (View Adapters). 
Эти адаптеры позволяют абстрагированно получить ViewPath и ViewPrefab у ECS-сущностей, не обращаясь напрямую к компонентам. 

### IEntityViewAdapter<TBehaviour, TEntity>
Интерфейс, определяющий контракт для адаптера:
- string ViewPath — путь к представлению (если указан)
- TBehaviour ViewPrefab — prefab компонента поведения (если указан)

### EntityViewAdapter<TBehaviour, TEntity>
Абстрактный класс, реализующий IEntityViewAdapter.
Содержит ссылку на сущность Entity, переданную через конструктор.

### Конкретные реализации на примере GameEntityViewAdapter
Используется для адаптации GameEntity.
Проверяет наличие компонентов ViewPath и ViewPrefab.



## Behaviours
Папка Behaviours содержит связующие MonoBehaviour-компоненты, которые соединяют ECS-сущности (Entitas) с GameObject'ами в Unity. 
Это часть паттерна "Entity View", где Unity-компоненты становятся визуальным/физическим представлением логических сущностей.


### IEntityBehaviour<T>
Интерфейс, определяющий контракт для поведения-связки между GameObject и сущностями Entitas
- T Entity — текущая привязанная сущность.
- void SetEntity(T entity) — связывает GameObject с ECS-сущностью.
- void ReleaseEntity() — разрывает эту связь.
- GameObject gameObject — для совместимости с Unity API.

### Конкретные реализации на примере GameEntityBehaviour
#### GameEntityBehaviour
Представление для GameEntity

Поведение:
- Присваивает сущность (SetEntity)
- Добавляет View компонент в сущность (AddView)
- Увеличивает ссылку владения (Retain)


Автоматически:
- Регистрирует все IEntityComponentRegistrar-компоненты (чтобы добавить поведение или логику в сущность).
- Регистрирует все Collider2D'ы в ICollisionRegistry, чтобы система коллизий знала, какая сущность за какой коллайдер отвечает.

Освобождение (ReleaseEntity):
- Отменяет регистрацию всех компонентов и коллайдеров.
- Уменьшает Retain у сущности.
- Обнуляет ссылку.


## Fabrics
Содержит обобщённые фабрики EntityViewFactory, создающие визуальное представление (MonoBehaviour) для ECS-сущностей.

### IEntityViewFactory<TBehaviour, TEntity>
Обобщенный Интерфейс, определяющий контракт для создания сущностей из префаба или пути Addressables
 - CreateViewForEntityFromAddresable - Создаёт view с использованием Addressables
 - CreateFromPrefab - Создаёт view из обычного префаба

### EntityViewFactory<TBehaviour, TEntity>
Зависимости (внедряются через Zenject):

IInstantiator — Zenject-интерфейс для создания объектов.

IAssetProvider — абстракция над Addressables (или другим ассет-лоадером).

- CreateViewForEntityFromAddresable - 
  - Загружает ассет по пути adapter.ViewPath.
  - Инстанцирует его через Zenject.
  - Вызывает SetEntity — привязывает view к ECS-сущности.

- CreateFromPrefab -
  - Работает аналогично, но ассет не загружается по пути, а передаётся напрямую как GameObject.



## Registrars
Содержит классы, через которые MonoBehaviour-компоненты могут регистрировать себя в сущности Entitas. 

### IEntityComponentRegistrar
Интерфейс, определяющий контракт для назначения компонентов в сущность и удаления их
- Добавить нужные компоненты в сущность при старте.
- Удалить компоненты при уничтожении.

### EntityDependent<TBehaviour, TEntity>
Используется как база для всех регистраторов, чтобы не дублировать доступ к Entity.
- Имеет ссылку на EntityView, реализующий IEntityBehaviour<TEntity>.
- Даёт доступ к самой сущности (Entity) через EntityView.Entity.

### EntityComponentRegistrar<TBehaviour, TEntity>
Абстрактный класс, реализующий IEntityComponentRegistrar
Используется, если тебе нужно вручную определить, что и как добавляется в сущность.

### AutoComponentRegistrar<TBehaviour, TEntity, TComponent>
Класс для автоматической регистрации компонентов

- Определяет название компонента
- Вызывает AddX(Component) и RemoveX() автоматически, основываясь на типе Unity-компонента
- Работает только при наличии соответствующих методов и свойств в коде TEntity (Entitas генерирует их автоматически, если компонент описан правильно).

### Реализация на примере TransformRegistrar
Создаем компонент для регистрации, название компаонента обязательно должно быть в формате
- 'НазваниеКомпонента'+Component
```csharp
    [Game] public class TransformComponent : IComponent { public Transform Value; }
```
Создаем класс регистратор, в наследующем классе Generic параметры:
- ...EntityBehaviour - тип связки Entity-Monobehaviour
- ...Entity - тип сущности
- Transform - тип компонента
```csharp
public sealed class TransfromRegistrar : AutoComponentRegistrar<GameEntityBehaviour, GameEntity, Transform> { }
```

## Systems
Папка для хранения систем связанных с BindViewFeature

### Bind...EntityViewFromPrefabSystem
Создаёт визуальное представление (EntityView) для Entity, 
если у сущности есть компонент ViewPrefab, но ещё нет View.

- Система отслеживает сущности с ViewPrefab, но без View.
- Через EntityViewFactory создаёт и привязывает визуальный объект к View.

### Bind...EntityViewFromPathSystem
Создаёт визуальное представление (EntityView) из Addressable, если у сущности есть ViewPath, но ещё нет View и ViewProcessed.

- Система отслеживает сущности с ViewPath, но без View и ViewProcessed.
- Загружает и привязывает EntityView из адресуемого ресурса.
- После создания помечает entity.isViewProcessed = true, чтобы не обрабатывать повторно.