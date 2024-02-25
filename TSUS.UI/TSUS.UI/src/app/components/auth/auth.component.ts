import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";
import { AuthService, DepartmentsService, UsersService } from "../../api/services";
import { BaseControlFlags, DepartmentRm, LoginDto, RegistrationDto, UserInfoRm } from "../../api/models";

@Component({
    selector: 'auth',
    standalone: true,
    imports: [CommonModule, RouterOutlet, ReactiveFormsModule, FormsModule, HttpClientModule, RouterModule],
    templateUrl: './auth.component.html',
    providers: [DepartmentsService, AuthService, UsersService],
    styleUrl: './auth.component.scss'
  })
export class AuthComponent implements OnInit {
    isRegistering: boolean = false;
    isVerifyingMail: boolean = false;
    isLoginValid: boolean = true;
    loginForm = this.formBuilder.group({
        email:  ['', Validators.required],
        password: ['', Validators.required]
    });
    registerForm: RegistrationDto = {
        email: '',
        userName: '',
        password: '',
        departmentId: 0
    };
    confirmPassword: string = '';
    departments: DepartmentRm[] = [];
    emailCode: number = 0;

    constructor(private formBuilder: FormBuilder, private departmentService: DepartmentsService, private authService: AuthService, private router: Router, private route: ActivatedRoute, private userService: UsersService) {
        this.departmentService.departmentsGet$Json({controlFlags: BaseControlFlags.$0})
            .subscribe((data: DepartmentRm[]) => this.departments = data);
    }

    ngOnInit(): void { }

    onModeChange() {
        this.isRegistering = !this.isRegistering;
    }

    // onFileSelected(event: any) {
    //     const selectedFile = event.target.files[0];
    //     if (selectedFile) {
    //         const reader = new FileReader();
    //         reader.onload = () => {
    //         const byteArray = new Uint8Array(reader.result as ArrayBuffer);
    //     };
    //     reader.readAsArrayBuffer(selectedFile);
    // }
    // }

    onSelectChange(event: any) {
        this.registerForm.departmentId = event?.target.value;
    }

    onLoginSubmit() {
        const loginDto: LoginDto = {
            email: this.loginForm.value.email ?? '',
            password: this.loginForm.value.password ?? ''
        }
        this.authService.authLoginPost$Json({body: loginDto})
            .subscribe((token) => {
                localStorage.setItem('token', token);
                this.userService.usersUserInfoGet$Json()
                    .subscribe((data: UserInfoRm) => {
                        if(data.isVerified == false) {
                            console.log(data);
                            this.isRegistering = !this.isRegistering;
                            this.isVerifyingMail = true;
                            this.authService.authEmailPost({email: loginDto.email}).subscribe(_ => (console.log("sent")), err => console.error(err));
                        } else {
                            this.router.navigate([''], {relativeTo: this.route})
                        }
                    });
                
            }, err => {
                this.loginForm.reset({email: loginDto.email ,password: ''});
                this.isLoginValid = false;
            });
    }

    onRegisterSubmit() {
        console.log(this.registerForm);
        console.log(this.departments);
        this.authService.authRegistrationPost({body: this.registerForm}).subscribe(_ => {
            this.isVerifyingMail = true;
            this.authService.authEmailPost({email: this.registerForm.email})
                .subscribe(_ => console.log("email sent!!"));
        });
    }

    onVerifySubmit() {
        this.authService.authVerifyPut({email: this.registerForm.email, code: this.emailCode})
            .subscribe(_ => this.router.navigate([""], {relativeTo: this.route}));
        
    }
}