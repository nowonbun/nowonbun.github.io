import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;

public class Server extends Thread {
	private InputStream receiver = null;
	private OutputStream sender = null;

	public Server(InputStream receiver, OutputStream sender) {
		this.receiver = receiver;
		this.sender = sender;
	}
	
	@Override
	public void run() {
		try {
			while (true) {
				boolean isBreak = false;
				//클라이언트로부터 메시지 대기
				byte[] data = new byte[4];
				receiver.read(data, 0, 4);
				String message = new String(data);
				message.replace("\0", "");
				String out = String.format("recieve - %s", message);
				System.out.println(out);
				//메시지가 EXIT면 종료한다.
				if("EXIT".equals(message)){
					isBreak = true;
				}
				message = "Welcome";
				data = message.getBytes();
				//Welcome 메시지 보냅니다.
				this.sender.write(data);
				if(isBreak){
					break;
				}
			}
		} catch (Throwable e) {
			e.printStackTrace();
		}finally{
			this.close();
		}
	}

	public void close(){
		try{
			this.receiver.close();
		}catch(Throwable e){
			e.printStackTrace();
		}
		try{
			this.sender.close();
		}catch(Throwable e){
			e.printStackTrace();
		}
	}

	public static void main(String... args) {
		// 자동 close
		try (ServerSocket server = new ServerSocket()) {
			// 서버 초기화
			InetSocketAddress ipep = new InetSocketAddress(9999);
			server.bind(ipep);
			System.out.println("Initialize complate");

			while (true) {
				// LISTEN 대기
				Socket client = server.accept();
				System.out.println("Connection");
				
				//Stream을 쓰레드로 넘기기
				Server serverThread = new Server(client.getInputStream(),client.getOutputStream());
				//쓰레드 시작(run을 호출한다.)
				serverThread.start();
			}
		} catch (Throwable e) {
			e.printStackTrace();
		}
	}

}
