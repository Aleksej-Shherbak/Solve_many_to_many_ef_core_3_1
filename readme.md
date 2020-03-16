## Решение проблемы Many-to-Many при работе с EF Core

Известно, что EF Core не поддерживате в полной мере отношения многие ко многим. Приходится создавать промежуточный 
класс. Часто встречается код, где создание такого класса происходит в контроллере. Обычно это выглядит как то так:

```
var advertisersManuallySending = new List<AdvertiserManuallySending>();

var advertisersList = await _advertiserRepository.Advertisers
    .Where(x => request.AdvertisersIds.Contains(x.AdvertiserId)).ToListAsync();

foreach (var advertiser in advertisersList)
{
    advertisersManuallySending.Add(new AdvertiserManuallySending
    {
        Advertiser = advertiser,
        ManuallySending = manuallySending
    });
}

manuallySending.AdvertiserManuallySendings = advertisersManuallySending;
```

Здесь есть сущность `Advertiser`, есть `ManuallySending`. Рекламодатель и Ручная рассылка. 
Допустим, у нас речь о некой системе отложенной рассылки пуш-уведомлений. Это сейчас не важно, пусть задача
будет абстрактной. Важно то, что у них отношения многие-ко-многим. Одна ручная рассылка может быть у многих 
рекламодателей и наоборот. Я думаю, это можно понять из кода выше по анименованию соединяющего класса  
`AdvertiserManuallySending`. 

Основная проблема состоит в том, что я не хотел бы видеть такой код в контроллерах. Можно вынести его в какие нибудь 
сервисы или фарбрики доменов. Но это все равно лишняя писанина и нарушение  DRY. 

Данный проект показывает, как изящно решить эту проблему. У нас есть `Student` и `Class`. 


Student
```
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<ClassStudent> ClassStudents { get; set; }
    
        [NotMapped]
        public IEnumerable<Class> Classes
        {
            get => ClassStudents.Select(r => r.Class);
            set => ClassStudents = value.Select(v => new ClassStudent()
            {
                ClassId = v.Id
            }).ToList();
        }
    }
```

Class 
```
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        public ICollection<ClassStudent> ClassStudents { get; set; }

        [NotMapped]
        public IEnumerable<Student> Students
        {
            get => ClassStudents.Select(r => r.Student);
            set => ClassStudents = value.Select(v => new ClassStudent()
            {
                StudentId = v.Id
            }).ToList();
        }
    }
```

Обратите внимание, как здесь организован доступ к связанным сущностям. 
Добавив репозитории, мы можем получить вот такой клиентский код: 

```
public IActionResult AddClasses()
{
    var student = _studentRepository.All.FirstOrDefault();
    var classes = _classRepository.All.ToList();

    student.Classes = classes;

    _studentRepository.Save(student);

    return Ok();
}
```

Буквально две строчки на то, чтоб установить отношения! Это намного лучше, чем первый пример в данной статье!
