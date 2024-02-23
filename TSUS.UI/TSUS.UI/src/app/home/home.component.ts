import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from "@angular/router";
import { AuthService, UsersService } from "../api/services";
import { HttpClientModule } from "@angular/common/http";
import { UserInfoRm } from "../api/models";
import { ClientService } from "../Services/clientService";
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
    notVerified = false;
    emailCode: number = null!;

    constructor(private userService: UsersService, private router: Router, private route: ActivatedRoute, public clientService: ClientService, private authService: AuthService) {
        userService.usersUserInfoGet$Json()
            .subscribe((data: UserInfoRm) => {
                this.clientService.client = data;
                console.log(this.clientService.client);

                if(data.isVerified == false){
                    this.notVerified = true;
                }
            }, err => {
                router.navigate(['auth'], {relativeTo: route})
            })
    }

    ngOnInit(): void {
    }

    onVerifySubmit() {
        this.authService.authVerifyPut({email: this.clientService.client.mail ?? '', code: this.emailCode})
            .subscribe(_ => {
                console.log("verified");
                this.notVerified = false;
            
            }
                );
    }
}