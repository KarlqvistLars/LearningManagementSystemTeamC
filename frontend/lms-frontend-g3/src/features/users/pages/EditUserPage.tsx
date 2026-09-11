import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { FormInput } from "../../../shared/components/FormInput";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormTitle } from "../../../shared/components/FormTitle";
import { Button } from "../../../shared/components/Button";
import { getUserById, updateUser } from "../api/userApi";
import type { User } from "../types/types";
import type { Role } from "../../roles/types/role";
import { getRoles } from "../../roles/api/roleApi";

export function EditUserPage() {
  const { userId } = useParams();
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);
  const [roles, setRoles] = useState<Role[]>([]);

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [city, setCity] = useState("");
  const [postalCode, setPostalCode] = useState("");
  const [address, setAddress] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [roleId, setRoleId] = useState("");
  const [isActive, setIsActive] = useState(true);

  useEffect(() => {
    if (!userId) return;

    const fetchData = async () => {
      try {
        const [user, roles] = await Promise.all([
          getUserById(userId),
          getRoles(),
        ]);

        setUser(user);
        setRoles(roles);

        setFirstName(user.firstName);
        setLastName(user.lastName);
        setCity(user.city ?? "");
        setPostalCode(user.postalCode ?? "");
        setAddress(user.address ?? "");
        setDateOfBirth(user.dateOfBirth?.split("T")[0] ?? "");
        setEmail(user.email);
        setPhoneNumber(user.phoneNumber ?? "");
        setRoleId(user.roleId);
        setIsActive(user.isActive);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  }, [userId]);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!userId) return;

    try {
      await updateUser(userId, {
        firstName,
        lastName,
        city: city || null,
        postalCode: postalCode || null,
        address: address || null,
        dateOfBirth: dateOfBirth || null,
        email,
        phoneNumber: phoneNumber || null,
        roleId,
        isActive,
      });

      navigate("/users");
    } catch (error) {
      console.error(error);
    }
  };

  const handleDelete = () => {
    if (!userId) return;

    // TODO: paused, using soft delete
    console.log(
      "entered handleDelete. Paused due to we r currently using soft delete",
    );
    console.log("Deleted user:", userId);
  };

  if (!user) {
    return <p>Loading...</p>;
  }

  const memberSince = new Date(user.createdAt).toLocaleDateString("sv-SE", {
    day: "numeric",
    month: "long",
    year: "numeric",
  });

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <div className="flex flex-1 flex-col gap-8 rounded-lg border border-border bg-menu px-10 py-10">
        <FormTitle title="Edit User" />

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
              <FormLabel htmlFor="memberSince" className="text-white">
                Member since
              </FormLabel>

              <FormInput
                id="memberSince"
                name="memberSince"
                value={memberSince}
                disabled
              />
            </div>

            <div>
              <FormLabel htmlFor="status" className="text-white">
                Status
              </FormLabel>

              <select
                id="status"
                name="status"
                value={isActive ? "active" : "inactive"}
                onChange={(event) =>
                  setIsActive(event.target.value === "active")
                }
                className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
              >
                <option value="active">Active</option>
                <option value="inactive">Inactive</option>
              </select>
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
            <Button type="submit" variant="list" color="edit">
              Edit
            </Button>

            <Button
              type="button"
              variant="list"
              color="delete"
              onClick={handleDelete}
            >
              Delete
            </Button>
          </div>
        </form>
      </div>
    </section>
  );
}
