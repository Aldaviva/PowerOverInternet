namespace PowerOverInternet.Services;

public interface OutletService {

    Task setPowerState(string hostname, bool turnOn);

}