/*1
 * public class TV
{
    public void On()
    {
        Console.WriteLine("ҚАЗІР ТЕЛЕДИДАР ҚОСУЛЫ");
    }
    public void Off()
    {
        Console.WriteLine("ТЕЛЕДИДАР Қазір өшули");
    }
    public void SetChannel(int channel)
    {
        Console.WriteLine($"Теледидао орнатылды{channel}.");
    }
}
public class AudioSystem
{
    private int _volume = 10;

    public void On()
    {
        Console.WriteLine("ҚАЗІР аулио ҚОСУЛЫ");
    }
    public void Off()
    {
        Console.WriteLine("ҚАЗІР аудио ошули");
    }
    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine($" Auido жүйесінің дыбыс деңгейі орнатылған{volume}");
    }
}

public class DVDPlayer
{
    public void Play()
    {
        Console.WriteLine("DVD ойнатылады");
    }
    public void Pause()
    {
        Console.WriteLine("DVD кідіртілд");
    }
    public void Stop()
    {
        Console.WriteLine("DVD токтатылд");
    }
}

public class GameConsole
{
    public void On()
    {
        Console.WriteLine("Ойын консолі Қазір Қосулы");
    }
    public void StartGame()
    {
        Console.WriteLine("Ойын басталады....");
    }
}

public class HomeTheaterFacade
{
    private TV _tv;
    private AudioSystem _audioSystem;
    private DVDPlayer _dvdplayer;
    private GameConsole _gameConsole;

    public HomeTheaterFacade(TV tV, AudioSystem audioSystem, DVDPlayer dvdplayer, GameConsole gameConsole)
    {
        _tv = tV;
        _audioSystem = audioSystem;
        _dvdplayer = dvdplayer;
        _gameConsole = gameConsole;
    }

    public void WatchMovie()
    {
        Console.WriteLine("Фильмді көру үшін орнату....");
        _tv.On();
        _audioSystem.On();
        _dvdplayer.Play();
    }
    public void EndMovie()
    {
        Console.WriteLine("фильмді Орнатуды Өшіру......");
        _dvdplayer.Stop();
        _audioSystem.Off();
        _tv.Off();
    }
    public void PlayGame()
    {
        Console.WriteLine("ойын ойнау Үшін Орнату.....");
        _tv.On();
        _audioSystem.On();
        _gameConsole.On();
        _gameConsole.StartGame();
    }

    public void ListenToMusic()
    {
        Console.WriteLine("музыка тыңдау Үшін Орнату.....");
        _tv.On();
        _audioSystem.On();
        _tv.SetChannel(1);
    }
    public void SetVolume(int volume)
    {
        _audioSystem.SetVolume(volume);
    }
    public void TurnOffAll()
    {
        Console.WriteLine("барлық жүйелерді Өшіру......");
        _dvdplayer.Stop();
        _gameConsole.On();
        _audioSystem.Off();
        _tv.Off();
    }
}

class Program
{
    static void Main(string[] args)
    {
        TV tV = new TV();
        AudioSystem audioSystem = new AudioSystem();
        DVDPlayer DVDPlayer = new DVDPlayer();
        GameConsole gameConsole = new GameConsole();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tV, audioSystem, DVDPlayer, gameConsole);

        homeTheater.WatchMovie();
        homeTheater.SetVolume(15);
        homeTheater.EndMovie();

        homeTheater.PlayGame();

        homeTheater.ListenToMusic();
        homeTheater.SetVolume(20);

        homeTheater.TurnOffAll();
    }
}



*/

public abstract class FileSystemComponent
{
    public string Name { get; protected set; }

    public FileSystemComponent(string name)
    {
        Name = name;
    }
    public abstract void Display();
    public abstract int GetSize();
}

public class File : FileSystemComponent
{
    private int _size;
    public File(string name, int size) : base(name)
    {
        _size = size;
    }

    public override void Display()
    {
        Console.WriteLine($"File {Name}, Size: {_size}KB");
    }
    public override int GetSize()
    {
        return _size;
    }
}

public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> _components = new List<FileSystemComponent>();

    public Directory(string name) : base(name) { }

    public void Add(FileSystemComponent component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
            Console.WriteLine($"Added {component.Name} to {Name}");
        }
    }

    public void Remove(FileSystemComponent component)
    {
        if (_components.Contains(component))
        {
            _components.Remove(component);
            Console.WriteLine($"Removed {component.Name} from {Name}");
        }
    }

    public override void Display()
    {
        Console.WriteLine($"Directory: {Name}");
        foreach (var component in _components)
        {
            component.Display();
        }
    }

    public override int GetSize()
    {
        int totalSize = 0;
        foreach (var component in _components)
        {
            totalSize += component.GetSize();
        }
        return totalSize;
    }
}

class Program
{
    static void Main(string[] args)
    {
        File file1 = new File("rau1.txt", 5611);
        File file2 = new File("rau2.txt", 5612);
        File file3 = new File("rau3.txt", 5231);

        Directory dir1 = new Directory("Documents");
        Directory dir2 = new Directory("Images");

        dir1.Add(file1);
        dir1.Add(file2);

        dir2.Add(file3);

        Directory rootDir = new Directory("Root");
        rootDir.Add(dir1);
        rootDir.Add(dir2);

        rootDir.Display();
        Console.WriteLine($"жалпы мөлшер: {rootDir.GetSize()}KB");

        dir1.Remove(file2);
        rootDir.Display();
        Console.WriteLine($"Жойылғаннан Кейін Жалпы өлшемі: {rootDir.GetSize()}KB");
    }
}