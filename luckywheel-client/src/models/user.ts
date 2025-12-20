import type {AuthUserDto} from "@/api/dto/auth-user-dto.ts";

export class User {
  email: string;
  emailConfirmed: boolean;

  constructor(email: string, isConfirmed: boolean) {
    this.email = email;
    this.emailConfirmed = isConfirmed;
  }

  public static fromAuthDto(authDto: AuthUserDto): User {
    return new User(authDto.email, authDto.emailConfirmed);
  }
}
