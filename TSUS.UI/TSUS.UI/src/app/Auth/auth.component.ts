import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";
import { AuthService, DepartmentsService } from "../api/services";
import { BaseControlFlags, DepartmentRm, LoginDto, RegistrationDto } from "../api/models";

@Component({
    selector: 'auth',
    standalone: true,
    imports: [CommonModule, RouterOutlet, ReactiveFormsModule, FormsModule, HttpClientModule, RouterModule],
    templateUrl: './auth.component.html',
    providers: [DepartmentsService, AuthService],
    styleUrl: './auth.component.scss'
  })
export class AuthComponent implements OnInit{
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

    constructor(private formBuilder: FormBuilder, private departmentService: DepartmentsService, private authService: AuthService, private router: Router, private route: ActivatedRoute) {
        this.departmentService.departmentsGet({controlFlags: BaseControlFlags.$0})
            .subscribe((data) => this.departments = data);
    }

    ngOnInit(): void {
    }

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
        this.authService.authLoginPost({body: loginDto})
            .subscribe((data) => console.log(data), err => {
                this.loginForm.reset({email: loginDto.email ,password: ''});
                this.isLoginValid = false;
            });
    }

    onRegisterSubmit() {
        console.log(this.registerForm);
        this.authService.authRegistrationPost({body: this.registerForm}).subscribe(_ => {
            this.isVerifyingMail = true;
            this.authService.authEmailPost({email: this.registerForm.email})
                .subscribe(_ => console.log("email sent!!"));
        });
    }

    onVerifySubmit() {
        this.authService.authVerifyPut({email: this.registerForm.email, code: this.emailCode})
            .subscribe(_ => console.log("Verified"));
        this.router.navigate([""], {relativeTo: this.route})
    }

}