import Panel from '../Panel';
import withAuth from "./WithAuth";
import Login from "../auth/login/Login";
import Register from "../auth/register/Register";
import Competition from "../rekabet/Competition";
import WooCommerce from "../wooCommerce/WooCommerce";

const authComponents = {Login, Register}
const protComponents = {Panel, Competition,WooCommerce}

const authenticatedComponents = {}
const protectedComponents = {}

for (const [key, value] of Object.entries(authComponents)) {
    authenticatedComponents[`Authenticated${key}`] = withAuth(value)
}

for (const [key, value] of Object.entries(protComponents)) {
    protectedComponents[`Protected${key}`] = withAuth(value)
    console.log(protectedComponents);
}

export const wrappedComponents = {...authenticatedComponents, ...protectedComponents}