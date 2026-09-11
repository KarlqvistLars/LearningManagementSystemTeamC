import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { FormInput } from "../../../shared/components/FormInput";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormTitle } from "../../../shared/components/FormTitle";
import { Button } from "../../../shared/components/Button";
import { createUser } from "../api/userApi";
import type { Role } from "../../roles/types/role";
import { getRoles } from "../../roles/api/roleApi";

export function CreateUserPage() {
  const navigate = useNavigate();

  const [roles, setRoles] = useState<Role[]>([]);
  const [errors, setErrors] = useState<Record<string, string[]>>({});

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [city, setCity] = useState("");
  const [postalCode, setPostalCode] = useState("");
  const [address, setAddress] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [password, setPassword] = useState("");
  const [roleId, setRoleId] = useState("");

  useEffect(() => {
    const fetchRoles = async () => {
      try {
        const roles = await getRoles();

        setRoles(roles);

        if (roles.length > 0) {
          setRoleId(roles[0].id);
        }
      } catch (error) {
        console.error(error);
      }
    };

    fetchRoles();
  }, []);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    setErrors({});

    try {
      await createUser({
        firstName,
        lastName,
        city: city || null,
        postalCode: postalCode || null,
        address: address || null,
        dateOfBirth: dateOfBirth || null,
        email,
        phoneNumber: phoneNumber || null,
        password,
        roleId,
      });

      navigate("/users");
    } catch (error) {
      if (error instanceof Error) {
        const validationError = error as Error & {
          details?: Record<string, string[]>;
        };

        setErrors(validationError.details ?? {});
        return;
      }

      console.error(error);
    }
  };

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
        <FormTitle title="Create User" />
        {Object.keys(errors).length > 0 && (
          <div className="rounded-md border border-red-500 bg-red-500/10 p-4">
            <p className="font-medium text-red-400">
              Please fix the following errors:
            </p>

            <ul className="mt-2 list-disc pl-5 text-red-400">
              {Object.values(errors)
                .flat()
                .map((message, index) => (
                  <li key={index}>{message}</li>
                ))}
            </ul>
          </div>
        )}
        <form onSubmit={handleSubmit} className="flex flex-1 flex-col">
          <div className="grid grid-cols-2 gap-x-10 gap-y-5">
            <div>
              <FormLabel htmlFor="firstName" className="text-white">
                First name
              </FormLabel>

              <FormInput
                id="firstName"
                name="firstName"
                value={firstName}
                onChange={(event) => setFirstName(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="lastName" className="text-white">
                Last name
              </FormLabel>

              <FormInput
                id="lastName"
                name="lastName"
                value={lastName}
                onChange={(event) => setLastName(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="city" className="text-white">
                City
              </FormLabel>

              <FormInput
                id="city"
                name="city"
                value={city}
                onChange={(event) => setCity(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="postalCode" className="text-white">
                Postal code
              </FormLabel>

              <FormInput
                id="postalCode"
                name="postalCode"
                value={postalCode}
                onChange={(event) => setPostalCode(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="address" className="text-white">
                Address
              </FormLabel>

              <FormInput
                id="address"
                name="address"
                value={address}
                onChange={(event) => setAddress(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="dateOfBirth" className="text-white">
                Date of birth
              </FormLabel>

              <FormInput
                id="dateOfBirth"
                name="dateOfBirth"
                type="date"
                value={dateOfBirth}
                onChange={(event) => setDateOfBirth(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="email" className="text-white">
                Email
              </FormLabel>

              <FormInput
                id="email"
                name="email"
                type="email"
                value={email}
                onChange={(event) => setEmail(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="phoneNumber" className="text-white">
                Phone number
              </FormLabel>

              <FormInput
                id="phoneNumber"
                name="phoneNumber"
                value={phoneNumber}
                onChange={(event) => setPhoneNumber(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="password" className="text-white">
                Password
              </FormLabel>

              <FormInput
                id="password"
                name="password"
                type="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
              />
            </div>

            <div>
              <FormLabel htmlFor="role" className="text-white">
                Role
              </FormLabel>

              <select
                id="role"
                name="roleId"
                value={roleId}
                onChange={(event) => setRoleId(event.target.value)}
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              >
                {roles.map((role) => (
                  <option key={role.id} value={role.id}>
                    {role.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          <div className="mt-auto flex justify-center gap-4 pt-10">
            <Button type="submit" variant="list" color="create">
              Create
            </Button>

            <Button
              type="button"
              variant="list"
              color="cancel"
              onClick={() => navigate("/users")}
            >
              Cancel
            </Button>
          </div>
        </form>
      </div>
    </section>
  );
}
