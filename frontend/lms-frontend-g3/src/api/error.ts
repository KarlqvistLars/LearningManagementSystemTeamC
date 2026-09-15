import type { ApiError } from "./types";

export class ApiRequestError extends Error {
  code: string;
  details?: Record<string, string[]>;

  constructor(error: ApiError) {
    super(error.message);

    this.name = "ApiRequestError";
    this.code = error.code;
    this.details = error.details;
  }
}
