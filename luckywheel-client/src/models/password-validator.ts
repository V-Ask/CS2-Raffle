import Regex from "@/helpers/constants/regex.ts";

export class PasswordValidator {

  private valid = true;

  constructor(public password: string) {
    if(!password) {
      this.valid = false;
    }
  }

  validateLengthy(minLength: number): PasswordValidator {
    this.valid = this.valid && this.password.length > minLength;
    return this;
  }

  validateUppercaseChar(): PasswordValidator {
    this.valid = this.valid && Regex.UPPERCASE_REGEX.test(this.password);
    return this;
  }

  validateLowercaseChar(): PasswordValidator {
    this.valid = this.valid && Regex.LOWERCASE_REGEX.test(this.password);
    return this;
  }

  validateDigit(): PasswordValidator {
    this.valid = this.valid && Regex.DIGIT_REGEX.test(this.password);
    return this;
  }

  validateNonAlphanumeric(): PasswordValidator {
    this.valid = this.valid && Regex.NON_ALPHANUMERIC_REGEX.test(this.password);
    return this;
  }

  validate(): boolean {
    return this.valid;
  }
}
