import React from "react";
import { connect } from "react-redux";
import { checkAuth } from "../../actions/auth";
import Login from "../auth/login/Login";

function withAuth(WrappedComponent) {
    class Wrapper extends React.Component {

        componentDidMount() {
            console.log("component did")
            this.props.checkAuth().then(result => console.log(result));
        }

        render() {
            if (!this.props.authChecked) {
                return <div>Yükleniyor...</div>;
            } else if (!this.props.loggedIn && this.props.protected) {
                return (
                    <>
                        <Login />
                        <p>You need to login to view this page.</p>
                    </>
                );
            } else {
                return <WrappedComponent {...this.props} />;
            }
        }
    }

    const mapStateToProps = ({authorization: { authChecked, loggedIn, currentUser }}) => {
        return { authChecked, loggedIn, currentUser };
    };

    const mapDispatchToProps = (dispatch) => {
        return {
            checkAuth: () => dispatch(checkAuth())
        };
    };

    return connect(mapStateToProps, mapDispatchToProps)(Wrapper);
}

export default withAuth;