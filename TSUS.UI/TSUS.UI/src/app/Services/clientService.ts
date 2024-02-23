import { Injectable } from "@angular/core";
import { UserInfoRm } from "../api/models";

@Injectable()
export class ClientService {
    client: UserInfoRm = {};
}