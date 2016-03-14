public class Main {
	public static void main(String[] args){
		try{
			new Main();
		}catch(Exception e){
			e.printStackTrace();
		}
	}
	public Main() throws Exception{
		Server server = new Server(80);
		server.start();
	}
}
