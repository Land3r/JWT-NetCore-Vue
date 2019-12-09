import common from './common'

import mainlayout from './layouts/mainlayout'

import indexpage from './pages/indexpage'
import loginpage from './pages/user/loginpage'
import forgotpasswordpage from './pages/user/forgotpasswordpage'
import resetpasswordpage from './pages/user/resetpasswordpage'
import registerpage from './pages/user/registerpage'
import activatepage from './pages/user/activatepage'

export default {
  ...common,

  mainlayout: {
    ...mainlayout
  },
  indexpage: {
    ...indexpage
  },
  loginpage: {
    ...loginpage
  },
  forgotpasswordpage: {
    ...forgotpasswordpage
  },
  resetpasswordpage: {
    ...resetpasswordpage
  },
  registerpage: {
    ...registerpage
  },
  activatepage: {
    ...activatepage
  },
  loginform: {
    section: {
      or: 'OR'
    },
    btn: {
      login: 'Login',
      createaccount: 'Create an account',
      forgotpassword: 'Password forgotten ?'
    },
    lbl: {
      username: 'Username',
      password: 'Password'
    },
    error: {
      username: 'Username field should be filled.',
      password: 'Password field should be filled.'
    }
  },
  lostpasswordform: {
    fillusernameofemail: 'Please fill in your username or email',
    section: {
      or: 'OR'
    },
    btn: {
      sendemail: 'Send email'
    },
    lbl: {
      username: 'Username',
      email: 'Email'
    },
    error: {
      username: 'Username or email field should be filled.',
      email: 'Emailfield should be a valid email address.'
    }
  },
  createaccountform: {
    section: {
      identity: 'Identity',
      password: 'Password'
    },
    btn: {
      createaccount: 'Create'
    },
    lbl: {
      username: 'Username',
      firstname: 'Firstname',
      lastname: 'Lastname',
      email: 'Email',
      email2: 'Please re-type your email',
      password: 'Password',
      password2: 'Please re-type your password'
    },
    error: {
      username: 'Username field should be filled.',
      firstname: 'Firstname field should be filled.',
      lastname: 'Lastname field should be filled.',
      email: 'Email field should be a valid email.',
      email2: 'Email field should be a valid email and match the previous email filled.',
      password: 'Password field should be filled.',
      password2: 'Password field should be filled and match the previous password filled.'
    }
  },
  createaccountpage: {
    error: {
      createfailure: 'Account creation failed.'
    },
    success: {
      createsuccess: 'Account created ! Check your emails to activate your account.'
    }
  }
}
