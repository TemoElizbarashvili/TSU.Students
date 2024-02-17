import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { RouterOutlet } from "@angular/router";
import { RegistrationDto } from "../../API/Interfaces/RegistrationDto";

@Component({
    selector: 'auth',
    standalone: true,
    imports: [CommonModule, RouterOutlet, ReactiveFormsModule, FormsModule],
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.scss'
  })
export class AuthComponent {
    isRegistering: boolean = false;
    isValid: boolean = true;
    loginForm: FormGroup;
    registerForm: RegistrationDto = {
        email: '',
        userName: '',
        password: '',
        profilePicture: new Uint8Array(),
        departmentId: 0
    };
    confirmPassword: string = '';
    selectedOption: string = '';

    constructor(private formBuilder: FormBuilder) {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    onModeChange() {
        this.isRegistering = !this.isRegistering;
    }

    onFileSelected(event: any) {
        const selectedFile = event.target.files[0];
        if (selectedFile) {
            const reader = new FileReader();
            reader.onload = () => {
            const byteArray = new Uint8Array(reader.result as ArrayBuffer);
            this.registerForm.profilePicture = byteArray;
        };
        reader.readAsArrayBuffer(selectedFile);
    }
    }

    onSelectChange(event: any) {
        this.registerForm.departmentId = event?.target.value;
    }
}