package hello;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@RestController
public class Application {

    @RequestMapping("/")
    public String home() {
        System.out.println(System.getProperties());
        return "Hosted on node: " + System.getenv("POD_NODE_NAME") + "\n"
            + " with name: " + System.getenv("POD_NAME");
    }

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }

}
