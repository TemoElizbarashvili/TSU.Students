import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from "@angular/router";
import { AuthService, UsersService } from "../../api/services";
import { HttpClientModule } from "@angular/common/http";
import { UserInfoRm } from "../../api/models";
import { ClientService } from "../../Services/clientService";
import { FormsModule } from "@angular/forms";

@Component({
    selector: 'home',
    standalone: true,
    imports: [CommonModule, RouterOutlet, HttpClientModule, RouterModule, FormsModule ],
    templateUrl: './home.component.html',
    providers: [UsersService, ClientService, AuthService],
    styleUrl: './home.component.scss'
  })
export class HomeComponent implements OnInit {

    constructor(private userService: UsersService, private router: Router, private route: ActivatedRoute, public clientService: ClientService, private authService: AuthService) {
        userService.usersUserInfoGet$Json()
            .subscribe((data: UserInfoRm) => {
                this.clientService.client = data;
                console.log(this.clientService.client);
                if(data.isVerified == false){
                    alert("please login again and verify your account!");
                    localStorage.clear();
                    this.router.navigate(['auth'], { relativeTo: route })
                }
            }, err => {
                router.navigate(['auth'], { relativeTo: route })
            });
    }

    ngOnInit(): void { }
}